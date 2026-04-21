using System.ComponentModel.DataAnnotations;

namespace RegistrationAuthority.Web.Domain.Requests;

/// <summary>
/// Модель запроса на создание заявки на выпуск сертификата.
/// </summary>
public sealed class CreateCertRequest
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    /// Common Name сертификата.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public string CommonName { get; set; } = string.Empty;

    /// <summary>
    /// Subject сертификата.
    /// </summary>
    [Required]
    [MaxLength(512)]
    public string Subject { get; set; } = string.Empty;
}
