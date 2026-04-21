using CertificationAuthority.Web.Domain.Entities;
using CertificationAuthority.Web.Domain.Requests;

namespace CertificationAuthority.Web.Services;

/// <summary>
/// Сервис управления сертификатами CA.
/// </summary>
public interface ICertificateService
{
    /// <summary>
    /// Выпускает сертификат.
    /// </summary>
    Task<Certificate> IssueAsync(IssueCertificateRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет сертификат.
    /// </summary>
    Task<Certificate?> UpdateAsync(Guid id, UpdateCertificateRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет сертификат.
    /// </summary>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает сертификат по идентификатору.
    /// </summary>
    Task<Certificate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает список сертификатов.
    /// </summary>
    Task<IReadOnlyCollection<Certificate>> GetAllAsync(CancellationToken cancellationToken = default);
}
