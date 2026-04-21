namespace RegistrationAuthority.Web.Domain.Requests;

/// <summary>
/// Запрос на создание заявки на отзыв сертификата.
/// </summary>
public sealed class CreateRevRequest
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Идентификатор сертификата.
    /// </summary>
    public Guid CertificateId { get; set; }

    /// <summary>
    /// Причина отзыва.
    /// </summary>
    public string Reason { get; set; } = string.Empty;
}
