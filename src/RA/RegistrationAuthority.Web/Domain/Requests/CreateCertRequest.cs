namespace RegistrationAuthority.Web.Domain.Requests;

/// <summary>
/// Запрос на создание заявки на выпуск сертификата.
/// </summary>
public sealed class CreateCertRequest
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Common Name сертификата.
    /// </summary>
    public string CommonName { get; set; } = string.Empty;

    /// <summary>
    /// Subject сертификата.
    /// </summary>
    public string Subject { get; set; } = string.Empty;
}
