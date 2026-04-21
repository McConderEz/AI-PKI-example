using CertificationAuthority.Web.Domain.Enums;

namespace CertificationAuthority.Web.Domain.Entities;

/// <summary>
/// Выпущенный сертификат центра сертификации.
/// </summary>
public sealed class Certificate
{
    /// <summary>
    /// Идентификатор сертификата.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Серийный номер сертификата.
    /// </summary>
    public string SerialNumber { get; set; } = string.Empty;

    /// <summary>
    /// Subject сертификата.
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор заявки на выпуск сертификата.
    /// </summary>
    public Guid CertRequestId { get; set; }

    /// <summary>
    /// Время выпуска сертификата.
    /// </summary>
    public DateTimeOffset IssuedAt { get; set; }

    /// <summary>
    /// Время окончания срока действия сертификата.
    /// </summary>
    public DateTimeOffset ExpiresAt { get; set; }

    /// <summary>
    /// Статус сертификата.
    /// </summary>
    public CertificateStatus Status { get; set; } = CertificateStatus.Active;

    /// <summary>
    /// Время создания записи.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Время обновления записи.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }
}
