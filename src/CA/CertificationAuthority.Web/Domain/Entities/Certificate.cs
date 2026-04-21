namespace CertificationAuthority.Web.Domain.Entities;

/// <summary>
/// Выпущенный сертификат.
/// </summary>
public sealed record Certificate
{
    /// <summary>
    /// Идентификатор сертификата.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Серийный номер сертификата.
    /// </summary>
    public string SerialNumber { get; init; } = string.Empty;

    /// <summary>
    /// Subject сертификата.
    /// </summary>
    public string Subject { get; init; } = string.Empty;

    /// <summary>
    /// Идентификатор заявки на выпуск сертификата.
    /// </summary>
    public Guid CertRequestId { get; init; }

    /// <summary>
    /// Время выпуска сертификата.
    /// </summary>
    public DateTimeOffset IssuedAt { get; init; }

    /// <summary>
    /// Время окончания срока действия сертификата.
    /// </summary>
    public DateTimeOffset ExpiresAt { get; init; }

    /// <summary>
    /// Статус сертификата.
    /// </summary>
    public string Status { get; init; } = "active";
}
