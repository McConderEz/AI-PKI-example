namespace RegistrationAuthority.Web.Domain.Entities;

/// <summary>
/// Запрос на выпуск сертификата.
/// </summary>
public sealed record CertRequest
{
    /// <summary>
    /// Идентификатор запроса.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Идентификатор пользователя, создавшего запрос.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Common Name для сертификата.
    /// </summary>
    public string CommonName { get; init; } = string.Empty;

    /// <summary>
    /// Subject запроса на сертификат.
    /// </summary>
    public string Subject { get; init; } = string.Empty;

    /// <summary>
    /// Текущее состояние запроса.
    /// </summary>
    public string Status { get; init; } = "pending";

    /// <summary>
    /// Идентификатор выпущенного сертификата, если заявка обработана.
    /// </summary>
    public Guid? CertificateId { get; init; }

    /// <summary>
    /// Время создания запроса.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Время последнего обновления заявки.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; init; }
}
