using RegistrationAuthority.Web.Domain.Enums;

namespace RegistrationAuthority.Web.Domain.Entities;

/// <summary>
/// Пользователь регистрационного центра.
/// </summary>
public sealed class User
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Полное имя пользователя.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Электронная почта пользователя.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Статус пользователя.
    /// </summary>
    public UserStatus Status { get; set; } = UserStatus.Active;

    /// <summary>
    /// Время создания записи.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Время последнего обновления записи.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }

    /// <summary>
    /// Заявки пользователя на выпуск сертификата.
    /// </summary>
    public List<CertRequest> CertRequests { get; set; } = [];

    /// <summary>
    /// Заявки пользователя на отзыв сертификата.
    /// </summary>
    public List<RevRequest> RevRequests { get; set; } = [];
}
