using System.ComponentModel.DataAnnotations;
using CertificationAuthority.Web.Domain.Enums;

namespace CertificationAuthority.Web.Domain.Requests;

/// <summary>
/// Модель запроса на обновление сертификата.
/// </summary>
public sealed class UpdateCertificateRequest
{
    /// <summary>
    /// Subject сертификата.
    /// </summary>
    [Required]
    [MaxLength(512)]
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Статус сертификата.
    /// </summary>
    public CertificateStatus Status { get; set; } = CertificateStatus.Active;
}
