using System.Collections.Concurrent;
using CertificationAuthority.Web.Domain.Entities;

namespace CertificationAuthority.Web.Infrastructure;

/// <summary>
/// In-memory хранилище данных CA.
/// </summary>
public sealed class InMemoryCaStore
{
    /// <summary>
    /// Коллекция выпущенных сертификатов.
    /// </summary>
    public ConcurrentDictionary<Guid, Certificate> Certificates { get; } = new();
}
