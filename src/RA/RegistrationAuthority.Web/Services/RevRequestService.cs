using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Domain.Enums;
using RegistrationAuthority.Web.Domain.Requests;
using RegistrationAuthority.Web.Infrastructure.Repositories;

namespace RegistrationAuthority.Web.Services;

/// <summary>
/// Сервис управления заявками на отзыв сертификатов на основе EF Core.
/// </summary>
public sealed class RevRequestService : IRevRequestService
{
    private readonly IUserRepository _userRepository;
    private readonly ICertificateRepository _certificateRepository;
    private readonly IRevRequestRepository _revRequestRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Инициализирует сервис заявок на отзыв сертификатов.
    /// </summary>
    public RevRequestService(
        IUserRepository userRepository,
        ICertificateRepository certificateRepository,
        IRevRequestRepository revRequestRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _certificateRepository = certificateRepository;
        _revRequestRepository = revRequestRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<RevRequest?> CreateAsync(CreateRevRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken).ConfigureAwait(false);
        var certificate = await _certificateRepository.GetByIdAsync(request.CertificateId, cancellationToken).ConfigureAwait(false);

        if (user is null || certificate is null)
        {
            return null;
        }

        var now = DateTimeOffset.UtcNow;
        var revRequest = new RevRequest
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            CertificateId = request.CertificateId,
            Reason = request.Reason.Trim(),
            Status = RevRequestStatus.Pending,
            CreatedAt = now,
            UpdatedAt = now
        };

        _revRequestRepository.Add(revRequest);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return revRequest;
    }

    /// <inheritdoc />
    public async Task<RevRequest?> UpdateAsync(Guid id, UpdateRevRequest request, CancellationToken cancellationToken = default)
    {
        var revRequest = await _revRequestRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        if (revRequest is null)
        {
            return null;
        }

        revRequest.Reason = request.Reason.Trim();
        revRequest.Status = request.Status;
        revRequest.UpdatedAt = DateTimeOffset.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return revRequest;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var revRequest = await _revRequestRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        if (revRequest is null)
        {
            return false;
        }

        _revRequestRepository.Remove(revRequest);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return true;
    }

    /// <inheritdoc />
    public Task<RevRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _revRequestRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<RevRequest>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _revRequestRepository.GetAllAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<RevRequest>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return _revRequestRepository.GetByUserAsync(userId, cancellationToken);
    }
}
