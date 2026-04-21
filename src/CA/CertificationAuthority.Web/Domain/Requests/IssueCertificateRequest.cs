namespace CertificationAuthority.Web.Domain.Requests;

/// <summary>
/// Запрос на выпуск сертификата.
/// </summary>
public sealed class IssueCertificateRequest
{
    /// <summary>
    /// Идентификатор заявки на выпуск сертификата.
    /// </summary>
    public Guid CertRequestId { get; set; }

    /// <summary>
    /// Subject сертификата.
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Количество дней действия сертификата.
    /// </summary>
    public int ValidDays { get; set; } = 365;
}
