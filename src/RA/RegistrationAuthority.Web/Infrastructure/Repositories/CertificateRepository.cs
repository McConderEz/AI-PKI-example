using Microsoft.EntityFrameworkCore;
using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Infrastructure.Persistence;

namespace RegistrationAuthority.Web.Infrastructure.Repositories;

/// <summary>
/// Репозиторий сертификатов.
/// </summary>
public sealed class CertificateRepository : ICertificateRepository
{
    private readonly RaDbContext _dbContext;

    /// <summary>
    /// Инициализирует репозиторий сертификатов.
    /// </summary>
    public CertificateRepository(RaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public void Add(Certificate certificate)
    {
        _dbContext.Certificates.Add(certificate);
    }

    /// <inheritdoc />
    public Task<Certificate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Certificates.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public Task<Certificate?> GetByCertRequestIdAsync(Guid certRequestId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Certificates.FirstOrDefaultAsync(x => x.CertRequestId == certRequestId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Certificate>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Certificates
            .OrderByDescending(x => x.IssuedAt)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
