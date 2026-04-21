using RegistrationAuthority.Web.Domain.Entities;

namespace RegistrationAuthority.Web.Infrastructure.Repositories;

/// <summary>
/// Репозиторий сертификатов.
/// </summary>
public interface ICertificateRepository
{
    /// <summary>
    /// Добавляет сертификат.
    /// </summary>
    /// <param name="certificate">Сертификат.</param>
    void Add(Certificate certificate);

    /// <summary>
    /// Получает сертификат по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сертификата.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<Certificate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает сертификат по идентификатору заявки.
    /// </summary>
    /// <param name="certRequestId">Идентификатор заявки на выпуск.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<Certificate?> GetByCertRequestIdAsync(Guid certRequestId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает все сертификаты.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<IReadOnlyCollection<Certificate>> GetAllAsync(CancellationToken cancellationToken = default);
}
