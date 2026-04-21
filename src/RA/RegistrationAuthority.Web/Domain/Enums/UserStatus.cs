namespace RegistrationAuthority.Web.Domain.Enums;

/// <summary>
/// Статус пользователя регистрационного центра.
/// </summary>
public enum UserStatus
{
    /// <summary>
    /// Пользователь активен.
    /// </summary>
    Active = 1,

    /// <summary>
    /// Пользователь временно заблокирован.
    /// </summary>
    Blocked = 2,

    /// <summary>
    /// Пользователь деактивирован.
    /// </summary>
    Archived = 3
}
