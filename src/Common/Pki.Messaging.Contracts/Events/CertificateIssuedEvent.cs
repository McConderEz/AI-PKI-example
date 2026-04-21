namespace Pki.Messaging.Contracts.Events;

/// <summary>
/// Событие об успешном выпуске сертификата.
/// </summary>
public sealed class CertificateIssuedEvent
{
    /// <summary>
    /// Идентификатор заявки на выпуск сертификата.
    /// </summary>
    public Guid CertRequestId { get; init; }

    /// <summary>
    /// Идентификатор выпущенного сертификата.
    /// </summary>
    public Guid CertificateId { get; init; }

    /// <summary>
    /// Серийный номер сертификата.
    /// </summary>
    public string SerialNumber { get; init; } = string.Empty;

    /// <summary>
    /// Время выпуска сертификата.
    /// </summary>
    public DateTimeOffset IssuedAt { get; init; }

    /// <summary>
    /// Время окончания действия сертификата.
    /// </summary>
    public DateTimeOffset ExpiresAt { get; init; }
}
