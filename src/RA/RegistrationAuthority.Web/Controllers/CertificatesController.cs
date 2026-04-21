using Microsoft.AspNetCore.Mvc;
using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Services;

namespace RegistrationAuthority.Web.Controllers;

/// <summary>
/// API чтения сертификатов в регистрационном центре.
/// </summary>
[ApiController]
[Route("api/certificates")]
public sealed class CertificatesController : ControllerBase
{
    private readonly ICertificateService _certificateService;

    /// <summary>
    /// Инициализирует контроллер сертификатов.
    /// </summary>
    public CertificatesController(ICertificateService certificateService)
    {
        _certificateService = certificateService;
    }

    /// <summary>
    /// Получает сертификат по идентификатору.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Certificate), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Certificate>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var certificate = await _certificateService.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        return certificate is null ? NotFound() : Ok(certificate);
    }

    /// <summary>
    /// Возвращает список сертификатов.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<Certificate>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<Certificate>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _certificateService.GetAllAsync(cancellationToken).ConfigureAwait(false));
    }
}
