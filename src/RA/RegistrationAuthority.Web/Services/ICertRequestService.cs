using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Domain.Requests;

namespace RegistrationAuthority.Web.Services;

/// <summary>
/// Сервис управления заявками на выпуск сертификатов.
/// </summary>
public interface ICertRequestService
{
    /// <summary>
    /// Создает заявку на выпуск сертификата.
    /// </summary>
    Task<CertRequest?> CreateAsync(CreateCertRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет заявку.
    /// </summary>
    Task<CertRequest?> UpdateAsync(Guid id, UpdateCertRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет заявку.
    /// </summary>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает заявку по идентификатору.
    /// </summary>
    Task<CertRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает все заявки.
    /// </summary>
    Task<IReadOnlyCollection<CertRequest>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает заявки пользователя.
    /// </summary>
    Task<IReadOnlyCollection<CertRequest>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Регистрирует сертификат, выпущенный центром сертификации.
    /// </summary>
    Task<bool> RegisterIssuedCertificateAsync(
        Guid certRequestId,
        Guid certificateId,
        string serialNumber,
        DateTimeOffset issuedAt,
        DateTimeOffset expiresAt,
        CancellationToken cancellationToken = default);
}
