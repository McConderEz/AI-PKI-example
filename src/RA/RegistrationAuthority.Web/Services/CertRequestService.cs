using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Domain.Enums;
using RegistrationAuthority.Web.Domain.Requests;
using RegistrationAuthority.Web.Infrastructure.Repositories;

namespace RegistrationAuthority.Web.Services;

/// <summary>
/// Сервис управления заявками на выпуск сертификатов на основе EF Core.
/// </summary>
public sealed class CertRequestService : ICertRequestService
{
    private readonly IUserRepository _userRepository;
    private readonly ICertRequestRepository _certRequestRepository;
    private readonly ICertificateRepository _certificateRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Инициализирует сервис заявок на выпуск.
    /// </summary>
    public CertRequestService(
        IUserRepository userRepository,
        ICertRequestRepository certRequestRepository,
        ICertificateRepository certificateRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _certRequestRepository = certRequestRepository;
        _certificateRepository = certificateRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<CertRequest?> CreateAsync(CreateCertRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken).ConfigureAwait(false);
        if (user is null)
        {
            return null;
        }

        var now = DateTimeOffset.UtcNow;
        var certRequest = new CertRequest
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            CommonName = request.CommonName.Trim(),
            Subject = request.Subject.Trim(),
            Status = CertRequestStatus.Pending,
            CreatedAt = now,
            UpdatedAt = now
        };

        _certRequestRepository.Add(certRequest);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return certRequest;
    }

    /// <inheritdoc />
    public async Task<CertRequest?> UpdateAsync(Guid id, UpdateCertRequest request, CancellationToken cancellationToken = default)
    {
        var certRequest = await _certRequestRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        if (certRequest is null)
        {
            return null;
        }

        certRequest.CommonName = request.CommonName.Trim();
        certRequest.Subject = request.Subject.Trim();
        certRequest.Status = request.Status;
        certRequest.UpdatedAt = DateTimeOffset.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return certRequest;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var certRequest = await _certRequestRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        if (certRequest is null)
        {
            return false;
        }

        _certRequestRepository.Remove(certRequest);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return true;
    }

    /// <inheritdoc />
    public Task<CertRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _certRequestRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<CertRequest>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _certRequestRepository.GetAllAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<CertRequest>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return _certRequestRepository.GetByUserAsync(userId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> RegisterIssuedCertificateAsync(
        Guid certRequestId,
        Guid certificateId,
        string serialNumber,
        DateTimeOffset issuedAt,
        DateTimeOffset expiresAt,
        CancellationToken cancellationToken = default)
    {
        var certRequest = await _certRequestRepository.GetByIdAsync(certRequestId, cancellationToken).ConfigureAwait(false);
        if (certRequest is null)
        {
            return false;
        }

        var existingCertificate = await _certificateRepository.GetByCertRequestIdAsync(certRequestId, cancellationToken).ConfigureAwait(false);
        if (existingCertificate is null)
        {
            existingCertificate = new Certificate
            {
                Id = certificateId,
                CertRequestId = certRequestId,
                SerialNumber = serialNumber,
                Subject = certRequest.Subject,
                IssuedAt = issuedAt,
                ExpiresAt = expiresAt,
                Status = CertificateStatus.Active,
                CreatedAt = DateTimeOffset.UtcNow
            };

            _certificateRepository.Add(existingCertificate);
        }
        else
        {
            existingCertificate.Id = certificateId;
            existingCertificate.SerialNumber = serialNumber;
            existingCertificate.Subject = certRequest.Subject;
            existingCertificate.IssuedAt = issuedAt;
            existingCertificate.ExpiresAt = expiresAt;
            existingCertificate.Status = CertificateStatus.Active;
        }

        certRequest.Status = CertRequestStatus.Issued;
        certRequest.UpdatedAt = DateTimeOffset.UtcNow;
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return true;
    }
}
