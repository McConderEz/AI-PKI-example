using CertificationAuthority.Web.Domain.Entities;
using CertificationAuthority.Web.Domain.Requests;
using CertificationAuthority.Web.Services;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Pki.Messaging.Contracts.Events;

namespace CertificationAuthority.Web.Controllers;

/// <summary>
/// API управления сертификатами центра сертификации.
/// </summary>
[ApiController]
[Route("api/certificates")]
public sealed class CertificatesController : ControllerBase
{
    private readonly ICertificateService _certificateService;
    private readonly IPublishEndpoint _publishEndpoint;

    /// <summary>
    /// Инициализирует контроллер сертификатов.
    /// </summary>
    /// <param name="certificateService">Сервис сертификатов.</param>
    /// <param name="publishEndpoint">Публикатор сообщений в брокер.</param>
    public CertificatesController(ICertificateService certificateService, IPublishEndpoint publishEndpoint)
    {
        _certificateService = certificateService;
        _publishEndpoint = publishEndpoint;
    }

    /// <summary>
    /// Выпускает сертификат по заявке.
    /// </summary>
    /// <param name="request">Запрос на выпуск сертификата.</param>
    /// <returns>Выпущенный сертификат.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Certificate), StatusCodes.Status201Created)]
    public async Task<ActionResult<Certificate>> Issue([FromBody] IssueCertificateRequest request)
    {
        var certificate = _certificateService.Issue(request);
        await _publishEndpoint.Publish(new CertificateIssuedEvent
        {
            CertRequestId = certificate.CertRequestId,
            CertificateId = certificate.Id,
            SerialNumber = certificate.SerialNumber,
            IssuedAt = certificate.IssuedAt,
            ExpiresAt = certificate.ExpiresAt
        }).ConfigureAwait(false);

        return CreatedAtAction(nameof(Issue), new { id = certificate.Id }, certificate);
    }
}
