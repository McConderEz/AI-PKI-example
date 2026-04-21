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
    /// <param name="revRequestService">Сервис заявок на отзыв сертификатов.</param>
    public RevRequestsController(IRevRequestService revRequestService)
    {
        _revRequestService = revRequestService;
    }

    /// <summary>
    /// Создает заявку на отзыв сертификата.
    /// </summary>
    /// <param name="request">Модель создания заявки.</param>
    /// <returns>Созданная заявка.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RevRequest), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<RevRequest> Create([FromBody] CreateRevRequest request)
    {
        var createdRequest = _revRequestService.Create(request);
        if (createdRequest is null)
        {
            return BadRequest($"Пользователь {request.UserId} не найден.");
        }

        return CreatedAtAction(nameof(GetById), new { id = createdRequest.Id }, createdRequest);
    }

    /// <summary>
    /// Получает заявку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заявки.</param>
    /// <returns>Заявка.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(RevRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<RevRequest> GetById(Guid id)
    {
        var revRequest = _revRequestService.GetById(id);
        if (revRequest is null)
        {
            return NotFound();
        }

        return Ok(revRequest);
    }

    /// <summary>
    /// Возвращает список всех заявок.
    /// </summary>
    /// <returns>Коллекция заявок.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<RevRequest>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyCollection<RevRequest>> GetAll()
    {
        return Ok(_revRequestService.GetAll());
    }

    /// <summary>
    /// Возвращает заявки конкретного пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Коллекция заявок пользователя.</returns>
    [HttpGet("/api/users/{userId:guid}/rev-requests")]
    [ProducesResponseType(typeof(IReadOnlyCollection<RevRequest>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyCollection<RevRequest>> GetByUser(Guid userId)
    {
        return Ok(_revRequestService.GetByUser(userId));
    }
}
