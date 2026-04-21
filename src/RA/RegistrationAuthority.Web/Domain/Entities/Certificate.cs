using RegistrationAuthority.Web.Domain.Enums;

namespace RegistrationAuthority.Web.Domain.Entities;

/// <summary>
/// Сертификат, полученный от центра сертификации.
/// </summary>
public sealed class Certificate
{
    /// <summary>
    /// Идентификатор сертификата.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор заявки на выпуск.
    /// </summary>
    public Guid CertRequestId { get; set; }

    /// <summary>
    /// Серийный номер сертификата.
    /// </summary>
    public string SerialNumber { get; set; } = string.Empty;

    /// <summary>
    /// Subject сертификата.
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Время выпуска сертификата.
    /// </summary>
    public DateTimeOffset IssuedAt { get; set; }

    /// <summary>
    /// Время окончания действия сертификата.
    /// </summary>
    public DateTimeOffset ExpiresAt { get; set; }

    /// <summary>
    /// Статус сертификата.
    /// </summary>
    public CertificateStatus Status { get; set; } = CertificateStatus.Active;

    /// <summary>
    /// Время создания записи сертификата в RA.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Связанная заявка на выпуск.
    /// </summary>
    public CertRequest? CertRequest { get; set; }

    /// <summary>
    /// Заявки на отзыв сертификата.
    /// </summary>
    public List<RevRequest> RevRequests { get; set; } = [];
}
