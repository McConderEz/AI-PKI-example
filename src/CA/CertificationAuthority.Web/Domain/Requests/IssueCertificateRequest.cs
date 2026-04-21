using System.ComponentModel.DataAnnotations;

namespace CertificationAuthority.Web.Domain.Requests;

/// <summary>
/// Модель запроса на выпуск сертификата.
/// </summary>
public sealed class IssueCertificateRequest
{
    /// <summary>
    /// Идентификатор заявки на выпуск сертификата.
    /// </summary>
    [Required]
    public Guid CertRequestId { get; set; }

    /// <summary>
    /// Subject сертификата.
    /// </summary>
    [Required]
    [MaxLength(512)]
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Количество дней действия сертификата.
    /// </summary>
    [Range(1, 3650)]
    public int ValidDays { get; set; } = 365;
}
