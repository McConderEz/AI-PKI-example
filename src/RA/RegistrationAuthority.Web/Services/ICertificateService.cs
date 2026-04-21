using RegistrationAuthority.Web.Domain.Entities;

namespace RegistrationAuthority.Web.Services;

/// <summary>
/// Сервис чтения и обновления сертификатов RA.
/// </summary>
public interface ICertificateService
{
    /// <summary>
    /// Получает сертификат по идентификатору.
    /// </summary>
    Task<Certificate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает список всех сертификатов.
    /// </summary>
    Task<IReadOnlyCollection<Certificate>> GetAllAsync(CancellationToken cancellationToken = default);
}
