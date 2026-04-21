namespace RegistrationAuthority.Web.Domain.Entities;

/// <summary>
/// Запрос на отзыв сертификата.
/// </summary>
public sealed record RevRequest
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
    /// Идентификатор сертификата, который требуется отозвать.
    /// </summary>
    public Guid CertificateId { get; init; }

    /// <summary>
    /// Причина отзыва сертификата.
    /// </summary>
    public string Reason { get; init; } = string.Empty;

    /// <summary>
    /// Текущее состояние запроса.
    /// </summary>
    public string Status { get; init; } = "pending";

    /// <summary>
    /// Время создания запроса.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }
}
