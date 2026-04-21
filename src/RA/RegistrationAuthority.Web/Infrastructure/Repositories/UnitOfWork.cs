using RegistrationAuthority.Web.Infrastructure.Persistence;

namespace RegistrationAuthority.Web.Infrastructure.Repositories;

/// <summary>
/// Единица работы.
/// </summary>
public sealed class UnitOfWork : IUnitOfWork
{
    private readonly RaDbContext _dbContext;

    /// <summary>
    /// Инициализирует единицу работы.
    /// </summary>
    public UnitOfWork(RaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
