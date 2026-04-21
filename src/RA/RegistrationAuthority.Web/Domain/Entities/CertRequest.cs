using RegistrationAuthority.Web.Domain.Enums;

namespace RegistrationAuthority.Web.Domain.Entities;

/// <summary>
/// Заявка на выпуск сертификата.
/// </summary>
public sealed class CertRequest
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
    /// Common Name сертификата.
    /// </summary>
    public string CommonName { get; set; } = string.Empty;

    /// <summary>
    /// Subject сертификата.
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Статус заявки.
    /// </summary>
    public CertRequestStatus Status { get; set; } = CertRequestStatus.Pending;

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
    /// Связанный сертификат, если заявка исполнена.
    /// </summary>
    public Certificate? Certificate { get; set; }
}
