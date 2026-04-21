namespace CertificationAuthority.Web.Domain.Enums;

/// <summary>
/// Статус сертификата.
/// </summary>
public enum CertificateStatus
{
    /// <summary>
    /// Сертификат активен.
    /// </summary>
    Active = 1,

    /// <summary>
    /// Сертификат отозван.
    /// </summary>
    Revoked = 2,

    /// <summary>
    /// Сертификат истек.
    /// </summary>
    Expired = 3,

    /// <summary>
    /// Сертификат приостановлен.
    /// </summary>
    Suspended = 4
}
