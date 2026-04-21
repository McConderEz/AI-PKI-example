using System.Collections.Concurrent;
using RegistrationAuthority.Web.Domain.Entities;

namespace RegistrationAuthority.Web.Infrastructure;

/// <summary>
/// In-memory хранилище данных RA.
/// </summary>
public sealed class InMemoryRaStore
{
    /// <summary>
    /// Коллекция пользователей.
    /// </summary>
    public ConcurrentDictionary<Guid, User> Users { get; } = new();

    /// <summary>
    /// Коллекция заявок на выпуск сертификатов.
    /// </summary>
    public ConcurrentDictionary<Guid, CertRequest> CertRequests { get; } = new();

    /// <summary>
    /// Коллекция заявок на отзыв сертификатов.
    /// </summary>
    public ConcurrentDictionary<Guid, RevRequest> RevRequests { get; } = new();
}
