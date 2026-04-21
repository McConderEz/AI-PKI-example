using System.ComponentModel.DataAnnotations;

namespace RegistrationAuthority.Web.Domain.Requests;

/// <summary>
/// Модель запроса на создание пользователя.
/// </summary>
public sealed class CreateUserRequest
{
    /// <summary>
    /// Полное имя пользователя.
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Электронная почта пользователя.
    /// </summary>
    [Required]
    [EmailAddress]
    [MaxLength(320)]
    public string Email { get; set; } = string.Empty;
}
