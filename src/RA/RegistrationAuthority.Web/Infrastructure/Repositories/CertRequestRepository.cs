using Microsoft.EntityFrameworkCore;
using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Infrastructure.Persistence;

namespace RegistrationAuthority.Web.Infrastructure.Repositories;

/// <summary>
/// Репозиторий заявок на выпуск сертификатов.
/// </summary>
public sealed class CertRequestRepository : ICertRequestRepository
{
    private readonly RaDbContext _dbContext;

    /// <summary>
    /// Инициализирует репозиторий заявок на выпуск сертификатов.
    /// </summary>
    public CertRequestRepository(RaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public void Add(CertRequest request)
    {
        _dbContext.CertRequests.Add(request);
    }

    /// <inheritdoc />
    public void Remove(CertRequest request)
    {
        _dbContext.CertRequests.Remove(request);
    }

    /// <inheritdoc />
    public Task<CertRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.CertRequests
            .Include(x => x.Certificate)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<CertRequest>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.CertRequests
            .Include(x => x.Certificate)
            .OrderBy(x => x.CreatedAt)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<CertRequest>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CertRequests
            .Include(x => x.Certificate)
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.CreatedAt)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
