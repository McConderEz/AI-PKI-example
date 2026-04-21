using Microsoft.AspNetCore.Mvc;
using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Domain.Requests;
using RegistrationAuthority.Web.Services;

namespace RegistrationAuthority.Web.Controllers;

/// <summary>
/// API управления заявками на отзыв сертификатов.
/// </summary>
[ApiController]
[Route("api/rev-requests")]
public sealed class RevRequestsController : ControllerBase
{
    private readonly IRevRequestService _revRequestService;

    /// <summary>
    /// Инициализирует контроллер заявок на отзыв сертификатов.
    /// </summary>
    public RevRequestsController(IRevRequestService revRequestService)
    {
        _revRequestService = revRequestService;
    }

    /// <summary>
    /// Создает заявку на отзыв сертификата.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(RevRequest), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RevRequest>> Create([FromBody] CreateRevRequest request, CancellationToken cancellationToken)
    {
        var created = await _revRequestService.CreateAsync(request, cancellationToken).ConfigureAwait(false);
        if (created is null)
        {
            return BadRequest("Пользователь или сертификат не найден.");
        }

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновляет заявку на отзыв сертификата.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(RevRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RevRequest>> Update(Guid id, [FromBody] UpdateRevRequest request, CancellationToken cancellationToken)
    {
        var updated = await _revRequestService.UpdateAsync(id, request, cancellationToken).ConfigureAwait(false);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>
    /// Удаляет заявку на отзыв сертификата.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _revRequestService.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
        return deleted ? NoContent() : NotFound();
    }

    /// <summary>
    /// Получает заявку по идентификатору.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(RevRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RevRequest>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var revRequest = await _revRequestService.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        return revRequest is null ? NotFound() : Ok(revRequest);
    }

    /// <summary>
    /// Возвращает список всех заявок.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<RevRequest>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<RevRequest>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _revRequestService.GetAllAsync(cancellationToken).ConfigureAwait(false));
    }

    /// <summary>
    /// Возвращает заявки конкретного пользователя.
    /// </summary>
    [HttpGet("/api/users/{userId:guid}/rev-requests")]
    [ProducesResponseType(typeof(IReadOnlyCollection<RevRequest>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<RevRequest>>> GetByUser(Guid userId, CancellationToken cancellationToken)
    {
        return Ok(await _revRequestService.GetByUserAsync(userId, cancellationToken).ConfigureAwait(false));
    }
}
