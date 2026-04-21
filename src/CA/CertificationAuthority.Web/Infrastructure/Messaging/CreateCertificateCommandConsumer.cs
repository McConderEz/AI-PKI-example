using CertificationAuthority.Web.Domain.Requests;
using CertificationAuthority.Web.Services;
using MassTransit;
using Pki.Messaging.Contracts.Commands;
using Pki.Messaging.Contracts.Events;

namespace CertificationAuthority.Web.Infrastructure.Messaging;

/// <summary>
/// Обработчик команды на выпуск сертификата.
/// </summary>
public sealed class CreateCertificateCommandConsumer : IConsumer<CreateCertificateCommand>
{
    private readonly ICertificateService _certificateService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<CreateCertificateCommandConsumer> _logger;

    /// <summary>
    /// Инициализирует обработчик команды выпуска сертификата.
    /// </summary>
    /// <param name="certificateService">Сервис сертификатов.</param>
    /// <param name="publishEndpoint">Публикатор сообщений в брокер.</param>
    /// <param name="logger">Логгер обработчика.</param>
    public CreateCertificateCommandConsumer(
        ICertificateService certificateService,
        IPublishEndpoint publishEndpoint,
        ILogger<CreateCertificateCommandConsumer> logger)
    {
        _certificateService = certificateService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    /// <summary>
    /// Обрабатывает команду на выпуск сертификата и публикует событие о результате.
    /// </summary>
    /// <param name="context">Контекст сообщения.</param>
    public async Task Consume(ConsumeContext<CreateCertificateCommand> context)
    {
        var message = context.Message;
        var certificate = await _certificateService.IssueAsync(new IssueCertificateRequest
        {
            CertRequestId = message.CertRequestId,
            Subject = message.Subject,
            ValidDays = 365
        }, context.CancellationToken).ConfigureAwait(false);

        await _publishEndpoint.Publish(new CertificateIssuedEvent
        {
            CertRequestId = message.CertRequestId,
            CertificateId = certificate.Id,
            SerialNumber = certificate.SerialNumber,
            IssuedAt = certificate.IssuedAt,
            ExpiresAt = certificate.ExpiresAt
        }, context.CancellationToken).ConfigureAwait(false);

        _logger.LogInformation(
            "Сертификат {CertificateId} выпущен по заявке {CertRequestId}, событие CertificateIssued опубликовано.",
            certificate.Id,
            message.CertRequestId);
    }
}
