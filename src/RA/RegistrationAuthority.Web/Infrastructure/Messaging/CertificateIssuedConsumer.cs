using MassTransit;
using Pki.Messaging.Contracts.Events;
using RegistrationAuthority.Web.Services;

namespace RegistrationAuthority.Web.Infrastructure.Messaging;

/// <summary>
/// Обработчик события о выпуске сертификата центром сертификации.
/// </summary>
public sealed class CertificateIssuedConsumer : IConsumer<CertificateIssuedEvent>
{
    private readonly ICertRequestService _certRequestService;
    private readonly ILogger<CertificateIssuedConsumer> _logger;

    /// <summary>
    /// Инициализирует обработчик события выпуска сертификата.
    /// </summary>
    /// <param name="certRequestService">Сервис заявок на выпуск сертификатов.</param>
    /// <param name="logger">Логгер обработчика.</param>
    public CertificateIssuedConsumer(ICertRequestService certRequestService, ILogger<CertificateIssuedConsumer> logger)
    {
        _certRequestService = certRequestService;
        _logger = logger;
    }

    /// <summary>
    /// Обрабатывает событие выпуска сертификата.
    /// </summary>
    /// <param name="context">Контекст сообщения.</param>
    public Task Consume(ConsumeContext<CertificateIssuedEvent> context)
    {
        var updated = _certRequestService.MarkIssued(context.Message.CertRequestId, context.Message.CertificateId);

        if (updated)
        {
            _logger.LogInformation(
                "Заявка {CertRequestId} обновлена как issued, сертификат {CertificateId}.",
                context.Message.CertRequestId,
                context.Message.CertificateId);
        }
        else
        {
            _logger.LogWarning(
                "Заявка {CertRequestId} не найдена при обработке события выпуска сертификата.",
                context.Message.CertRequestId);
        }

        return Task.CompletedTask;
    }
}
