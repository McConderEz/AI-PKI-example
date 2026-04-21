using System.ComponentModel.DataAnnotations;

namespace RegistrationAuthority.Web.Domain.Requests;

/// <summary>
/// Модель запроса на создание заявки на отзыв сертификата.
/// </summary>
public sealed class CreateRevRequest
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    /// Идентификатор сертификата.
    /// </summary>
    [Required]
    public Guid CertificateId { get; set; }

    /// <summary>
    /// Причина отзыва.
    /// </summary>
    [Required]
    [MaxLength(512)]
    public string Reason { get; set; } = string.Empty;
}
