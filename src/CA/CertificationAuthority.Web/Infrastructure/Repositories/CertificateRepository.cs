using CertificationAuthority.Web.Domain.Entities;
using CertificationAuthority.Web.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CertificationAuthority.Web.Infrastructure.Repositories;

/// <summary>
/// Репозиторий сертификатов CA.
/// </summary>
public sealed class CertificateRepository : ICertificateRepository
{
    private readonly CaDbContext _dbContext;

    /// <summary>
    /// Инициализирует репозиторий сертификатов.
    /// </summary>
    public CertificateRepository(CaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public void Add(Certificate certificate)
    {
        _dbContext.Certificates.Add(certificate);
    }

    /// <inheritdoc />
    public void Remove(Certificate certificate)
    {
        _dbContext.Certificates.Remove(certificate);
    }

    /// <inheritdoc />
    public Task<Certificate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Certificates.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
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
