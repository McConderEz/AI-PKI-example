using RegistrationAuthority.Web.Domain.Enums;

namespace RegistrationAuthority.Web.Domain.Entities;

/// <summary>
/// Заявка на отзыв сертификата.
/// </summary>
public sealed class RevRequest
{
    /// <summary>
    /// Идентификатор заявки.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Идентификатор сертификата для отзыва.
    /// </summary>
    public Guid CertificateId { get; set; }

    /// <summary>
    /// Причина отзыва.
    /// </summary>
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// Статус заявки на отзыв.
    /// </summary>
    public RevRequestStatus Status { get; set; } = RevRequestStatus.Pending;

    /// <summary>
    /// Время создания заявки.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Время последнего обновления заявки.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }

    /// <summary>
    /// Пользователь, создавший заявку.
    /// </summary>
    public User? User { get; set; }

    /// <summary>
    /// Сертификат, который требуется отозвать.
    /// </summary>
    public Certificate? Certificate { get; set; }
}
