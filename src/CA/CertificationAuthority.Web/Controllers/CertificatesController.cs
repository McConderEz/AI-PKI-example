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
    public CertificatesController(ICertificateService certificateService, IPublishEndpoint publishEndpoint)
    {
        _certificateService = certificateService;
        _publishEndpoint = publishEndpoint;
    }

    /// <summary>
    /// Выпускает сертификат по заявке.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Certificate), StatusCodes.Status201Created)]
    public async Task<ActionResult<Certificate>> Issue([FromBody] IssueCertificateRequest request, CancellationToken cancellationToken)
    {
        var certificate = await _certificateService.IssueAsync(request, cancellationToken).ConfigureAwait(false);

        await _publishEndpoint.Publish(new CertificateIssuedEvent
        {
            CertRequestId = certificate.CertRequestId,
            CertificateId = certificate.Id,
            SerialNumber = certificate.SerialNumber,
            IssuedAt = certificate.IssuedAt,
            ExpiresAt = certificate.ExpiresAt
        }, cancellationToken).ConfigureAwait(false);

        return CreatedAtAction(nameof(GetById), new { id = certificate.Id }, certificate);
    }

    /// <summary>
    /// Обновляет сертификат.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Certificate), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Certificate>> Update(Guid id, [FromBody] UpdateCertificateRequest request, CancellationToken cancellationToken)
    {
        var updated = await _certificateService.UpdateAsync(id, request, cancellationToken).ConfigureAwait(false);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>
    /// Удаляет сертификат.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _certificateService.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
        return deleted ? NoContent() : NotFound();
    }

    /// <summary>
    /// Получает сертификат по идентификатору.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Certificate), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Certificate>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var certificate = await _certificateService.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        return certificate is null ? NotFound() : Ok(certificate);
    }

    /// <summary>
    /// Возвращает список сертификатов.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<Certificate>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<Certificate>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _certificateService.GetAllAsync(cancellationToken).ConfigureAwait(false));
    }
}
