using System.Text.Json;
using System.Text.RegularExpressions;
using Assistant.Web.Application.Abstractions;
using Assistant.Web.Application.Models;
using Assistant.Web.Infrastructure.Clients;
using Assistant.Web.Infrastructure.Clients.Models;
using Assistant.Web.Infrastructure.LLM.Formats;
using Assistant.Web.Infrastructure.LLM.Prompts;
using Microsoft.Extensions.Logging;

namespace Assistant.Web.Infrastructure.Providers;

/// <summary>
/// Реализация провайдера LLM уровня инфраструктуры.
/// </summary>
public sealed partial class LLMProvider : ILLMProvider
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly ILLMClient _llmClient;
    private readonly ILogger<LLMProvider> _logger;

    /// <summary>
    /// Инициализирует экземпляр провайдера LLM.
    /// </summary>
    /// <param name="llmClient">Refit-клиент для вызова LLM API.</param>
    /// <param name="logger">Логгер провайдера.</param>
    public LLMProvider(ILLMClient llmClient, ILogger<LLMProvider> logger)
    {
        _llmClient = llmClient ?? throw new ArgumentNullException(nameof(llmClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Выполняет генерацию текстового ответа через API Ollama.
    /// </summary>
    /// <param name="model">Имя модели.</param>
    /// <param name="message">Сообщение пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сгенерированный ответ модели.</returns>
    public async Task<string> GenerateAsync(string model, string message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Начат вызов внешнего LLM API для текстового ответа. Модель: {Model}, длина сообщения: {MessageLength}.",
            model,
            message.Length);

        try
        {
            var response = await _llmClient.ChatAsync(model, message, cancellationToken).ConfigureAwait(false);
            var content = response.Message?.Content ?? string.Empty;

            _logger.LogInformation(
                "Текстовый ответ от внешнего LLM API получен. Модель: {Model}, длина ответа: {ResponseLength}.",
                model,
                content.Length);

            return content;
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Ошибка при вызове внешнего LLM API для текстового ответа. Модель: {Model}.",
                model);

            throw;
        }
    }

    /// <summary>
    /// Выполняет структурированную PKI-классификацию запроса через API Ollama.
    /// </summary>
    /// <param name="model">Имя модели.</param>
    /// <param name="message">Сообщение пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Структурированный результат классификации.</returns>
    public async Task<PkiIntentResult> ClassifyPkiIntentAsync(string model, string message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Начат вызов внешнего LLM API для PKI-классификации. Модель: {Model}, длина сообщения: {MessageLength}.",
            model,
            message.Length);

        try
        {
            var normalizedMessage = NormalizePkiAliases(message);
            var request = new OllamaChatRequest
            {
                Model = model,
                Stream = false,
                Format = LLMResponseFormats.PkiRouterIntentFormat,
                Options = new OllamaChatOptions
                {
                    Temperature = 0
                },
                Messages =
                [
                    new OllamaChatMessageRequest
                    {
                        Role = "system",
                        Content = LLMRolePrompts.PkiRouterSystemPrompt
                    },
                    new OllamaChatMessageRequest
                    {
                        Role = "user",
                        Content = normalizedMessage
                    }
                ]
            };

            var response = await _llmClient.ChatRawAsync(request, cancellationToken).ConfigureAwait(false);
            var content = response.Message?.Content ?? string.Empty;

            if (string.IsNullOrWhiteSpace(content))
            {
                _logger.LogWarning("Внешний LLM API вернул пустой structured-ответ PKI. Модель: {Model}.", model);
                throw new InvalidOperationException("Модель вернула пустой structured-ответ.");
            }

            var parsed = JsonSerializer.Deserialize<PkiIntentResult>(content, JsonOptions);
            if (parsed is null)
            {
                _logger.LogWarning("Не удалось десериализовать structured-ответ PKI. Модель: {Model}, ответ: {RawResponse}.", model, content);
                throw new InvalidOperationException("Не удалось разобрать structured-ответ модели.");
            }

            _logger.LogInformation(
                "PKI-классификация успешно получена от внешнего LLM API. Модель: {Model}, интент: {Intent}, confidence: {Confidence}.",
                model,
                parsed.Intent,
                parsed.Confidence);

            return parsed;
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Ошибка при вызове внешнего LLM API для PKI-классификации. Модель: {Model}.",
                model);

            throw;
        }
    }

    /// <summary>
    /// Нормализует распространенные русские сокращения PKI к каноническим обозначениям.
    /// </summary>
    /// <param name="input">Исходный текст запроса пользователя.</param>
    /// <returns>Текст с добавленными каноническими синонимами для повышения точности.</returns>
    private static string NormalizePkiAliases(string input)
    {
        var output = input;

        if (RegistrationAuthorityRegex().IsMatch(output))
        {
            output += " [канонизация: RA = Registration Authority = центр регистрации = ЦР]";
        }

        if (CertificationAuthorityRegex().IsMatch(output))
        {
            output += " [канонизация: CA = Certification Authority = удостоверяющий центр = центр сертификации = ЦС]";
        }

        return output;
    }

    [GeneratedRegex(@"(?i)\b(цр|ra|registration\s*authority|центр\s*регистрац[иия])\b")]
    private static partial Regex RegistrationAuthorityRegex();

    [GeneratedRegex(@"(?i)\b(цс|ca|certification\s*authority|удостоверяющ(ий|его)\s*центр|центр\s*сертификац[иия])\b")]
    private static partial Regex CertificationAuthorityRegex();
}
