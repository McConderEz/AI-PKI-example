using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Pki.Messaging.Contracts.Commands;
using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Domain.Requests;
using RegistrationAuthority.Web.Services;

namespace RegistrationAuthority.Web.Controllers;

/// <summary>
/// API управления заявками на выпуск сертификатов.
/// </summary>
[ApiController]
[Route("api/cert-requests")]
public sealed class CertRequestsController : ControllerBase
{
    private readonly ICertRequestService _certRequestService;
    private readonly IPublishEndpoint _publishEndpoint;

    /// <summary>
    /// Инициализирует контроллер заявок на выпуск сертификатов.
    /// </summary>
    /// <param name="certRequestService">Сервис заявок на выпуск сертификатов.</param>
    /// <param name="publishEndpoint">Публикатор сообщений в брокер.</param>
    public CertRequestsController(ICertRequestService certRequestService, IPublishEndpoint publishEndpoint)
    {
        _certRequestService = certRequestService;
        _publishEndpoint = publishEndpoint;
    }

    /// <summary>
    /// Создает заявку на выпуск сертификата.
    /// </summary>
    /// <param name="request">Модель создания заявки.</param>
    /// <returns>Созданная заявка.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CertRequest), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CertRequest>> Create([FromBody] CreateCertRequest request)
    {
        var createdRequest = _certRequestService.Create(request);
        if (createdRequest is null)
        {
            return BadRequest($"Пользователь {request.UserId} не найден.");
        }

        await _publishEndpoint.Publish(new CreateCertificateCommand
        {
            CertRequestId = createdRequest.Id,
            UserId = createdRequest.UserId,
            CommonName = createdRequest.CommonName,
            Subject = createdRequest.Subject,
            RequestedAt = createdRequest.CreatedAt
        }).ConfigureAwait(false);

        return CreatedAtAction(nameof(GetById), new { id = createdRequest.Id }, createdRequest);
    }

    /// <summary>
    /// Получает заявку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заявки.</param>
    /// <returns>Заявка.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CertRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<CertRequest> GetById(Guid id)
    {
        var certRequest = _certRequestService.GetById(id);
        if (certRequest is null)
        {
            return NotFound();
        }

        return Ok(certRequest);
    }

    /// <summary>
    /// Возвращает список всех заявок.
    /// </summary>
    /// <returns>Коллекция заявок.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<CertRequest>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyCollection<CertRequest>> GetAll()
    {
        return Ok(_certRequestService.GetAll());
    }

    /// <summary>
    /// Возвращает заявки конкретного пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Коллекция заявок пользователя.</returns>
    [HttpGet("/api/users/{userId:guid}/cert-requests")]
    [ProducesResponseType(typeof(IReadOnlyCollection<CertRequest>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyCollection<CertRequest>> GetByUser(Guid userId)
    {
        return Ok(_certRequestService.GetByUser(userId));
    }
}
