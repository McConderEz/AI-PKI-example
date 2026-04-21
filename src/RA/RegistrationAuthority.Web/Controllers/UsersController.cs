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
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Создает пользователя.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
    public async Task<ActionResult<User>> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var createdUser = await _userService.CreateAsync(request, cancellationToken).ConfigureAwait(false);
        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
    }

    /// <summary>
    /// Обновляет пользователя.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<User>> Update(Guid id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var updatedUser = await _userService.UpdateAsync(id, request, cancellationToken).ConfigureAwait(false);
        return updatedUser is null ? NotFound() : Ok(updatedUser);
    }

    /// <summary>
    /// Удаляет пользователя.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _userService.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
        return deleted ? NoContent() : NotFound();
    }

    /// <summary>
    /// Получает пользователя по идентификатору.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<User>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        return user is null ? NotFound() : Ok(user);
    }

    /// <summary>
    /// Возвращает список пользователей.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<User>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<User>>> GetAll(CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllAsync(cancellationToken).ConfigureAwait(false);
        return Ok(users);
    }
}
