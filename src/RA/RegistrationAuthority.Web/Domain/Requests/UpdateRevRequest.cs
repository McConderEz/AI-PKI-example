using System.ComponentModel.DataAnnotations;
using RegistrationAuthority.Web.Domain.Enums;

namespace RegistrationAuthority.Web.Domain.Requests;

/// <summary>
/// Модель запроса на обновление заявки на отзыв сертификата.
/// </summary>
public sealed class UpdateRevRequest
{
    /// <summary>
    /// Причина отзыва.
    /// </summary>
    [Required]
    [MaxLength(512)]
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// Статус заявки на отзыв.
    /// </summary>
    public RevRequestStatus Status { get; set; } = RevRequestStatus.Pending;
}
