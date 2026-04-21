using RegistrationAuthority.Web.Domain.Entities;

namespace RegistrationAuthority.Web.Infrastructure.Repositories;

/// <summary>
/// Репозиторий заявок на выпуск сертификатов.
/// </summary>
public interface ICertRequestRepository
{
    /// <summary>
    /// Добавляет заявку.
    /// </summary>
    /// <param name="request">Заявка.</param>
    void Add(CertRequest request);

    /// <summary>
    /// Удаляет заявку.
    /// </summary>
    /// <param name="request">Заявка.</param>
    void Remove(CertRequest request);

    /// <summary>
    /// Получает заявку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заявки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<CertRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает все заявки.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<IReadOnlyCollection<CertRequest>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает заявки пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<IReadOnlyCollection<CertRequest>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
