using CertificationAuthority.Web.Infrastructure.Persistence;

namespace CertificationAuthority.Web.Infrastructure.Repositories;

/// <summary>
/// Единица работы CA.
/// </summary>
public sealed class UnitOfWork : IUnitOfWork
{
    private readonly CaDbContext _dbContext;

    /// <summary>
    /// Инициализирует единицу работы.
    /// </summary>
    public UnitOfWork(CaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
