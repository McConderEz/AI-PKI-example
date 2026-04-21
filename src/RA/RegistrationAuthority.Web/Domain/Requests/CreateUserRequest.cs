namespace RegistrationAuthority.Web.Domain.Requests;

/// <summary>
/// Запрос на создание пользователя.
/// </summary>
public sealed class CreateUserRequest
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
