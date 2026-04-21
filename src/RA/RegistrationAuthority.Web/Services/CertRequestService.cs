using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Domain.Requests;
using RegistrationAuthority.Web.Infrastructure;

namespace RegistrationAuthority.Web.Services;

/// <summary>
/// In-memory реализация сервиса заявок на выпуск сертификатов.
/// </summary>
public sealed class CertRequestService : ICertRequestService
{
    private readonly InMemoryRaStore _store;

    /// <summary>
    /// Инициализирует сервис заявок на выпуск сертификатов.
    /// </summary>
    /// <param name="store">In-memory хранилище RA.</param>
    public CertRequestService(InMemoryRaStore store)
    {
        _store = store;
    }

    /// <inheritdoc />
    public CertRequest? Create(CreateCertRequest request)
    {
        if (!_store.Users.ContainsKey(request.UserId))
        {
            return null;
        }

        var certRequest = new CertRequest
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            CommonName = request.CommonName,
            Subject = request.Subject,
            Status = "pending",
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _store.CertRequests[certRequest.Id] = certRequest;
        return certRequest;
    }

    /// <inheritdoc />
    public CertRequest? GetById(Guid id)
    {
        return _store.CertRequests.TryGetValue(id, out var certRequest) ? certRequest : null;
    }

    /// <inheritdoc />
    public IReadOnlyCollection<CertRequest> GetAll()
    {
        return _store.CertRequests.Values.OrderBy(x => x.CreatedAt).ToArray();
    }

    /// <inheritdoc />
    public IReadOnlyCollection<CertRequest> GetByUser(Guid userId)
    {
        return _store.CertRequests.Values
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.CreatedAt)
            .ToArray();
    }

    /// <inheritdoc />
    public bool MarkIssued(Guid certRequestId, Guid certificateId)
    {
        if (!_store.CertRequests.TryGetValue(certRequestId, out var existingRequest))
        {
            return false;
        }

        _store.CertRequests[certRequestId] = existingRequest with
        {
            Status = "issued",
            CertificateId = certificateId,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        return true;
    }
}
