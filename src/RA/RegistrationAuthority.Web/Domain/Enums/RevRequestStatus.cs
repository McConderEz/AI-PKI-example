namespace RegistrationAuthority.Web.Domain.Enums;

/// <summary>
/// Статус заявки на отзыв сертификата.
/// </summary>
public enum RevRequestStatus
{
    /// <summary>
    /// Ожидает обработки.
    /// </summary>
    Pending = 1,

    /// <summary>
    /// Одобрена.
    /// </summary>
    Approved = 2,

    /// <summary>
    /// Отклонена.
    /// </summary>
    Rejected = 3,

    /// <summary>
    /// Выполнена.
    /// </summary>
    Completed = 4,

    /// <summary>
    /// Отменена.
    /// </summary>
    Cancelled = 5
}
