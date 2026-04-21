using CertificationAuthority.Web.Domain.Entities;

namespace CertificationAuthority.Web.Infrastructure.Repositories;

/// <summary>
/// Репозиторий сертификатов CA.
/// </summary>
public interface ICertificateRepository
{
    /// <summary>
    /// Добавляет сертификат.
    /// </summary>
    void Add(Certificate certificate);

    /// <summary>
    /// Удаляет сертификат.
    /// </summary>
    void Remove(Certificate certificate);

    /// <summary>
    /// Получает сертификат по идентификатору.
    /// </summary>
    Task<Certificate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает все сертификаты.
    /// </summary>
    Task<IReadOnlyCollection<Certificate>> GetAllAsync(CancellationToken cancellationToken = default);
}
