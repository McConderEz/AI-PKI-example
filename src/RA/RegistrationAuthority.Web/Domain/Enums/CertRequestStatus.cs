namespace RegistrationAuthority.Web.Domain.Enums;

/// <summary>
/// Статус заявки на выпуск сертификата.
/// </summary>
public enum CertRequestStatus
{
    /// <summary>
    /// Черновик.
    /// </summary>
    Draft = 1,

    /// <summary>
    /// Ожидает обработки.
    /// </summary>
    Pending = 2,

    /// <summary>
    /// В обработке.
    /// </summary>
    Processing = 3,

    /// <summary>
    /// Сертификат выпущен.
    /// </summary>
    Issued = 4,

    /// <summary>
    /// Выпуск отклонен.
    /// </summary>
    Rejected = 5,

    /// <summary>
    /// Заявка отменена.
    /// </summary>
    Cancelled = 6
}
