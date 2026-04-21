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
        return ConsumeInternalAsync(context);
    }

    private async Task ConsumeInternalAsync(ConsumeContext<CertificateIssuedEvent> context)
    {
        var updated = await _certRequestService.RegisterIssuedCertificateAsync(
                context.Message.CertRequestId,
                context.Message.CertificateId,
                context.Message.SerialNumber,
                context.Message.IssuedAt,
                context.Message.ExpiresAt,
                context.CancellationToken)
            .ConfigureAwait(false);

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
    }
}
