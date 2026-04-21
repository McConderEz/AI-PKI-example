using Microsoft.AspNetCore.Mvc;
using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Domain.Requests;
using RegistrationAuthority.Web.Services;

namespace RegistrationAuthority.Web.Controllers;

/// <summary>
/// API управления пользователями регистрационного центра.
/// </summary>
[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// Инициализирует контроллер пользователей.
    /// </summary>
    /// <param name="userService">Сервис управления пользователями.</param>
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Создает пользователя.
    /// </summary>
    /// <param name="request">Модель создания пользователя.</param>
    /// <returns>Созданный пользователь.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
    public ActionResult<User> Create([FromBody] CreateUserRequest request)
    {
        var createdUser = _userService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
    }

    /// <summary>
    /// Обновляет пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="request">Модель обновления пользователя.</param>
    /// <returns>Обновленный пользователь.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<User> Update(Guid id, [FromBody] UpdateUserRequest request)
    {
        var updatedUser = _userService.Update(id, request);
        if (updatedUser is null)
        {
            return NotFound();
        }

        return Ok(updatedUser);
    }

    /// <summary>
    /// Получает пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <returns>Пользователь.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<User> GetById(Guid id)
    {
        var user = _userService.GetById(id);
        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    /// <summary>
    /// Возвращает список пользователей.
    /// </summary>
    /// <returns>Коллекция пользователей.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<User>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyCollection<User>> GetAll()
    {
        return Ok(_userService.GetAll());
    }
}
