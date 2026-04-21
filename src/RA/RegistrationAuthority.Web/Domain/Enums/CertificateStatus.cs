namespace RegistrationAuthority.Web.Domain.Enums;

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
    /// Срок действия сертификата истек.
    /// </summary>
    Expired = 3,

    /// <summary>
    /// Сертификат приостановлен.
    /// </summary>
    Suspended = 4
}
