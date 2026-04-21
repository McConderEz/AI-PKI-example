using RegistrationAuthority.Web.Domain.Entities;

namespace RegistrationAuthority.Web.Infrastructure.Repositories;

/// <summary>
/// Репозиторий заявок на отзыв сертификатов.
/// </summary>
public interface IRevRequestRepository
{
    /// <summary>
    /// Добавляет заявку.
    /// </summary>
    /// <param name="request">Заявка.</param>
    void Add(RevRequest request);

    /// <summary>
    /// Удаляет заявку.
    /// </summary>
    /// <param name="request">Заявка.</param>
    void Remove(RevRequest request);

    /// <summary>
    /// Получает заявку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заявки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<RevRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает все заявки.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<IReadOnlyCollection<RevRequest>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает заявки пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<IReadOnlyCollection<RevRequest>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
