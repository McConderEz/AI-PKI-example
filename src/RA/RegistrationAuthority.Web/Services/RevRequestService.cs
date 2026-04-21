using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Domain.Requests;
using RegistrationAuthority.Web.Infrastructure;

namespace RegistrationAuthority.Web.Services;

/// <summary>
/// In-memory реализация сервиса заявок на отзыв сертификатов.
/// </summary>
public sealed class RevRequestService : IRevRequestService
{
    private readonly InMemoryRaStore _store;

    /// <summary>
    /// Инициализирует сервис заявок на отзыв сертификатов.
    /// </summary>
    /// <param name="store">In-memory хранилище RA.</param>
    public RevRequestService(InMemoryRaStore store)
    {
        _store = store;
    }

    /// <inheritdoc />
    public RevRequest? Create(CreateRevRequest request)
    {
        if (!_store.Users.ContainsKey(request.UserId))
        {
            return null;
        }

        var revRequest = new RevRequest
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            CertificateId = request.CertificateId,
            Reason = request.Reason,
            Status = "pending",
            CreatedAt = DateTimeOffset.UtcNow
        };

        _store.RevRequests[revRequest.Id] = revRequest;
        return revRequest;
    }

    /// <inheritdoc />
    public RevRequest? GetById(Guid id)
    {
        return _store.RevRequests.TryGetValue(id, out var revRequest) ? revRequest : null;
    }

    /// <inheritdoc />
    public IReadOnlyCollection<RevRequest> GetAll()
    {
        return _store.RevRequests.Values.OrderBy(x => x.CreatedAt).ToArray();
    }

    /// <inheritdoc />
    public IReadOnlyCollection<RevRequest> GetByUser(Guid userId)
    {
        return _store.RevRequests.Values
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.CreatedAt)
            .ToArray();
    }
}
