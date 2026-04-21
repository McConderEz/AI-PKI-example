using CertificationAuthority.Web.Domain.Entities;
using CertificationAuthority.Web.Domain.Requests;
using CertificationAuthority.Web.Infrastructure;

namespace CertificationAuthority.Web.Services;

/// <summary>
/// In-memory реализация сервиса сертификатов CA.
/// </summary>
public sealed class CertificateService : ICertificateService
{
    private readonly InMemoryCaStore _store;

    /// <summary>
    /// Инициализирует сервис сертификатов.
    /// </summary>
    /// <param name="store">In-memory хранилище CA.</param>
    public CertificateService(InMemoryCaStore store)
    {
        _store = store;
    }

    /// <inheritdoc />
    public Certificate Issue(IssueCertificateRequest request)
    {
        var issuedAt = DateTimeOffset.UtcNow;
        var certificate = new Certificate
        {
            Id = Guid.NewGuid(),
            CertRequestId = request.CertRequestId,
            Subject = request.Subject,
            SerialNumber = Guid.NewGuid().ToString("N").ToUpperInvariant(),
            IssuedAt = issuedAt,
            ExpiresAt = issuedAt.AddDays(request.ValidDays <= 0 ? 365 : request.ValidDays),
            Status = "active"
        };

        _store.Certificates[certificate.Id] = certificate;
        return certificate;
    }
}
