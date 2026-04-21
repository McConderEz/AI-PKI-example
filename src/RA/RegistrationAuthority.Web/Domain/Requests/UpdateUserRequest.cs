namespace RegistrationAuthority.Web.Domain.Requests;

/// <summary>
/// Запрос на обновление пользователя.
/// </summary>
public sealed class UpdateUserRequest
{
    /// <summary>
    /// Полное имя пользователя.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Электронная почта пользователя.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}
