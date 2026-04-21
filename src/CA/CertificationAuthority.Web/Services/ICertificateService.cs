using CertificationAuthority.Web.Domain.Entities;
using CertificationAuthority.Web.Domain.Requests;

namespace CertificationAuthority.Web.Services;

/// <summary>
/// Сервис управления сертификатами CA.
/// </summary>
public interface ICertificateService
{
    /// <summary>
    /// Выпускает сертификат.
    /// </summary>
    /// <param name="request">Запрос на выпуск.</param>
    /// <returns>Выпущенный сертификат.</returns>
    Certificate Issue(IssueCertificateRequest request);
}
