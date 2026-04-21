using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Infrastructure.Repositories;

namespace RegistrationAuthority.Web.Services;

/// <summary>
/// Сервис чтения сертификатов RA.
/// </summary>
public sealed class CertificateService : ICertificateService
{
    private readonly ICertificateRepository _certificateRepository;

    /// <summary>
    /// Инициализирует сервис сертификатов.
    /// </summary>
    public CertificateService(ICertificateRepository certificateRepository)
    {
        _certificateRepository = certificateRepository;
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
