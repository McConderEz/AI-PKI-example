using Microsoft.EntityFrameworkCore;
using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Infrastructure.Persistence;

namespace RegistrationAuthority.Web.Infrastructure.Repositories;

/// <summary>
/// Репозиторий заявок на отзыв сертификатов.
/// </summary>
public sealed class RevRequestRepository : IRevRequestRepository
{
    private readonly RaDbContext _dbContext;

    /// <summary>
    /// Инициализирует репозиторий заявок на отзыв.
    /// </summary>
    public RevRequestRepository(RaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public void Add(RevRequest request)
    {
        _dbContext.RevRequests.Add(request);
    }

    /// <inheritdoc />
    public void Remove(RevRequest request)
    {
        _dbContext.RevRequests.Remove(request);
    }

    /// <inheritdoc />
    public Task<RevRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.RevRequests.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<RevRequest>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.RevRequests
            .OrderByDescending(x => x.CreatedAt)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<RevRequest>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.RevRequests
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
