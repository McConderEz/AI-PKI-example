namespace RegistrationAuthority.Web.Domain.Entities;

/// <summary>
/// Пользователь в контексте регистрационного центра.
/// </summary>
public sealed record User
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Полное имя пользователя.
    /// </summary>
    public string FullName { get; init; } = string.Empty;

    /// <summary>
    /// Электронная почта пользователя.
    /// </summary>
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// Время создания записи.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Время последнего обновления записи.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; init; }
}
