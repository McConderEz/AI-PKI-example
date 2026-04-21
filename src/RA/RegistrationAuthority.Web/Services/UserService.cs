using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Domain.Requests;
using RegistrationAuthority.Web.Infrastructure.Repositories;

namespace RegistrationAuthority.Web.Services;

/// <summary>
/// Сервис пользователей RA на основе EF Core.
/// </summary>
public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Инициализирует сервис пользователей.
    /// </summary>
    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<User> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName.Trim(),
            Email = request.Email.Trim().ToLowerInvariant(),
            CreatedAt = now,
            UpdatedAt = now
        };

        _userRepository.Add(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return user;
    }

    /// <inheritdoc />
    public async Task<User?> UpdateAsync(Guid id, UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        if (user is null)
        {
            return null;
        }

        user.FullName = request.FullName.Trim();
        user.Email = request.Email.Trim().ToLowerInvariant();
        user.Status = request.Status;
        user.UpdatedAt = DateTimeOffset.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return user;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        if (user is null)
        {
            return false;
        }

        _userRepository.Remove(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return true;
    }

    /// <inheritdoc />
    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _userRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _userRepository.GetAllAsync(cancellationToken);
    }
}
