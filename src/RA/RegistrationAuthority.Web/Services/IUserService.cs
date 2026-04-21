using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Domain.Requests;

namespace RegistrationAuthority.Web.Services;

/// <summary>
/// Сервис управления пользователями RA.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Создает пользователя.
    /// </summary>
    Task<User> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет пользователя.
    /// </summary>
    Task<User?> UpdateAsync(Guid id, UpdateUserRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет пользователя.
    /// </summary>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает пользователя по идентификатору.
    /// </summary>
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает список пользователей.
    /// </summary>
    Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken = default);
}
