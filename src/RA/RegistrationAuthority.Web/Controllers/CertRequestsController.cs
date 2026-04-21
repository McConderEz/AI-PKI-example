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
    private readonly ILogger<CertRequestsController> _logger;

    /// <summary>
    /// Инициализирует контроллер заявок на выпуск сертификатов.
    /// </summary>
    public CertRequestsController(
        ICertRequestService certRequestService,
        IPublishEndpoint publishEndpoint,
        ILogger<CertRequestsController> logger)
    {
        _certRequestService = certRequestService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    /// <summary>
    /// Создает заявку на выпуск сертификата и отправляет команду в CA.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CertRequest), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CertRequest>> Create([FromBody] CreateCertRequest request, CancellationToken cancellationToken)
    {
        var createdRequest = await _certRequestService.CreateAsync(request, cancellationToken).ConfigureAwait(false);
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
        }, cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Заявка {CertRequestId} создана и команда CreateCertificateCommand отправлена.", createdRequest.Id);
        return CreatedAtAction(nameof(GetById), new { id = createdRequest.Id }, createdRequest);
    }

    /// <summary>
    /// Обновляет заявку на выпуск сертификата.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CertRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CertRequest>> Update(Guid id, [FromBody] UpdateCertRequest request, CancellationToken cancellationToken)
    {
        var updated = await _certRequestService.UpdateAsync(id, request, cancellationToken).ConfigureAwait(false);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>
    /// Удаляет заявку на выпуск сертификата.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _certRequestService.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
        return deleted ? NoContent() : NotFound();
    }

    /// <summary>
    /// Получает заявку по идентификатору.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CertRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CertRequest>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var certRequest = await _certRequestService.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        return certRequest is null ? NotFound() : Ok(certRequest);
    }

    /// <summary>
    /// Возвращает список всех заявок.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<CertRequest>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<CertRequest>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _certRequestService.GetAllAsync(cancellationToken).ConfigureAwait(false));
    }

    /// <summary>
    /// Возвращает заявки конкретного пользователя.
    /// </summary>
    [HttpGet("/api/users/{userId:guid}/cert-requests")]
    [ProducesResponseType(typeof(IReadOnlyCollection<CertRequest>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<CertRequest>>> GetByUser(Guid userId, CancellationToken cancellationToken)
    {
        return Ok(await _certRequestService.GetByUserAsync(userId, cancellationToken).ConfigureAwait(false));
    }
}
