using CertificationAuthority.Web.Domain.Entities;
using CertificationAuthority.Web.Domain.Enums;
using CertificationAuthority.Web.Domain.Requests;
using CertificationAuthority.Web.Infrastructure.Repositories;

namespace CertificationAuthority.Web.Services;

/// <summary>
/// Сервис сертификатов CA на основе EF Core.
/// </summary>
public sealed class CertificateService : ICertificateService
{
    private readonly ICertificateRepository _certificateRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Инициализирует сервис сертификатов.
    /// </summary>
    public CertificateService(ICertificateRepository certificateRepository, IUnitOfWork unitOfWork)
    {
        _certificateRepository = certificateRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Certificate> IssueAsync(IssueCertificateRequest request, CancellationToken cancellationToken = default)
    {
        var issuedAt = DateTimeOffset.UtcNow;
        var certificate = new Certificate
        {
            Id = Guid.NewGuid(),
            CertRequestId = request.CertRequestId,
            Subject = request.Subject.Trim(),
            SerialNumber = Guid.NewGuid().ToString("N").ToUpperInvariant(),
            IssuedAt = issuedAt,
            ExpiresAt = issuedAt.AddDays(request.ValidDays),
            Status = CertificateStatus.Active,
            CreatedAt = issuedAt,
            UpdatedAt = issuedAt
        };

        _certificateRepository.Add(certificate);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return certificate;
    }

    /// <inheritdoc />
    public async Task<Certificate?> UpdateAsync(Guid id, UpdateCertificateRequest request, CancellationToken cancellationToken = default)
    {
        var certificate = await _certificateRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        if (certificate is null)
        {
            return null;
        }

        certificate.Subject = request.Subject.Trim();
        certificate.Status = request.Status;
        certificate.UpdatedAt = DateTimeOffset.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return certificate;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var certificate = await _certificateRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        if (certificate is null)
        {
            return false;
        }

        _certificateRepository.Remove(certificate);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return true;
    }

    /// <inheritdoc />
    public Task<Certificate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _certificateRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<Certificate>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _certificateRepository.GetAllAsync(cancellationToken);
    }
}
