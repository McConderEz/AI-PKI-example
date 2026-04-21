using System.ComponentModel.DataAnnotations;
using RegistrationAuthority.Web.Domain.Enums;

namespace RegistrationAuthority.Web.Domain.Requests;

/// <summary>
/// Модель запроса на обновление пользователя.
/// </summary>
public sealed class UpdateUserRequest
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

    /// <summary>
    /// Статус пользователя.
    /// </summary>
    public UserStatus Status { get; set; } = UserStatus.Active;
}
