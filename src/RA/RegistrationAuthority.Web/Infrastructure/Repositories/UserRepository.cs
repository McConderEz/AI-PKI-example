using Microsoft.EntityFrameworkCore;
using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Infrastructure.Persistence;

namespace RegistrationAuthority.Web.Infrastructure.Repositories;

/// <summary>
/// Репозиторий пользователей.
/// </summary>
public sealed class UserRepository : IUserRepository
{
    private readonly RaDbContext _dbContext;

    /// <summary>
    /// Инициализирует репозиторий пользователей.
    /// </summary>
    public UserRepository(RaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public void Add(User user)
    {
        _dbContext.Users.Add(user);
    }

    /// <inheritdoc />
    public void Remove(User user)
    {
        _dbContext.Users.Remove(user);
    }

    /// <inheritdoc />
    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .OrderBy(x => x.CreatedAt)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
