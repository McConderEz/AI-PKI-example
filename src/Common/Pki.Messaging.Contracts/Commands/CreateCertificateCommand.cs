namespace Pki.Messaging.Contracts.Commands;

/// <summary>
/// Команда на выпуск сертификата в центре сертификации.
/// </summary>
public sealed class CreateCertificateCommand
{
    /// <summary>
    /// Идентификатор заявки на выпуск сертификата.
    /// </summary>
    public Guid CertRequestId { get; init; }

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Common Name сертификата.
    /// </summary>
    public string CommonName { get; init; } = string.Empty;

    /// <summary>
    /// Subject сертификата.
    /// </summary>
    public string Subject { get; init; } = string.Empty;

    /// <summary>
    /// Время формирования команды.
    /// </summary>
    public DateTimeOffset RequestedAt { get; init; }
}
