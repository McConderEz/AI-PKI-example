using System.ComponentModel.DataAnnotations;
using RegistrationAuthority.Web.Domain.Enums;

namespace RegistrationAuthority.Web.Domain.Requests;

/// <summary>
/// Модель запроса на обновление заявки на выпуск сертификата.
/// </summary>
public sealed class UpdateCertRequest
{
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

    /// <summary>
    /// Статус заявки.
    /// </summary>
    public CertRequestStatus Status { get; set; } = CertRequestStatus.Pending;
}
