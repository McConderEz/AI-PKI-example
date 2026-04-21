using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Domain.Requests;
using RegistrationAuthority.Web.Infrastructure;

namespace RegistrationAuthority.Web.Services;

/// <summary>
/// In-memory реализация сервиса пользователей RA.
/// </summary>
public sealed class UserService : IUserService
{
    private readonly InMemoryRaStore _store;

    /// <summary>
    /// Инициализирует сервис пользователей.
    /// </summary>
    /// <param name="store">In-memory хранилище RA.</param>
    public UserService(InMemoryRaStore store)
    {
        _store = store;
    }

    /// <inheritdoc />
    public User Create(CreateUserRequest request)
    {
        var now = DateTimeOffset.UtcNow;
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Email = request.Email,
            CreatedAt = now,
            UpdatedAt = now
        };

        _store.Users[user.Id] = user;
        return user;
    }

    /// <inheritdoc />
    public User? Update(Guid id, UpdateUserRequest request)
    {
        if (!_store.Users.TryGetValue(id, out var existingUser))
        {
            return null;
        }

        var updatedUser = existingUser with
        {
            FullName = request.FullName,
            Email = request.Email,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _store.Users[id] = updatedUser;
        return updatedUser;
    }

    /// <inheritdoc />
    public User? GetById(Guid id)
    {
        return _store.Users.TryGetValue(id, out var user) ? user : null;
    }

    /// <inheritdoc />
    public IReadOnlyCollection<User> GetAll()
    {
        return _store.Users.Values.OrderBy(x => x.CreatedAt).ToArray();
    }
}
