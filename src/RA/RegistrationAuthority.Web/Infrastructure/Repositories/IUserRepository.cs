using RegistrationAuthority.Web.Domain.Entities;

namespace RegistrationAuthority.Web.Infrastructure.Repositories;

/// <summary>
/// Репозиторий пользователей.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Добавляет пользователя.
    /// </summary>
    /// <param name="user">Пользователь.</param>
    void Add(User user);

    /// <summary>
    /// Удаляет пользователя.
    /// </summary>
    /// <param name="user">Пользователь.</param>
    void Remove(User user);

    /// <summary>
    /// Получает пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает всех пользователей.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken = default);
}
