using Assistant.Web.Application.Abstractions;
using Assistant.Web.Application.Models;
using Microsoft.Extensions.Logging;

namespace Assistant.Web.Application.Services;

/// <summary>
/// Сервис прикладного слоя для работы с чатом LLM.
/// </summary>
public sealed class LLMChatService
{
    /// <summary>
    /// Значение модели по умолчанию.
    /// </summary>
    public const string DefaultModel = "llama3.1:8b";

    private readonly ILLMProvider _llmProvider;
    private readonly ILogger<LLMChatService> _logger;

    /// <summary>
    /// Инициализирует экземпляр сервиса чата LLM.
    /// </summary>
    /// <param name="llmProvider">Провайдер доступа к LLM.</param>
    /// <param name="logger">Логгер сервиса.</param>
    public LLMChatService(ILLMProvider llmProvider, ILogger<LLMChatService> logger)
    {
        _llmProvider = llmProvider ?? throw new ArgumentNullException(nameof(llmProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Отправляет сообщение в LLM и возвращает текстовый ответ.
    /// </summary>
    /// <param name="model">Имя модели. Если не передано, используется модель по умолчанию.</param>
    /// <param name="message">Сообщение пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Ответ модели.</returns>
    public async Task<string> ChatAsync(string? model, string message, CancellationToken cancellationToken = default)
    {
        var selectedModel = string.IsNullOrWhiteSpace(model) ? DefaultModel : model.Trim();

        _logger.LogInformation(
            "Начата обработка текстового запроса к LLM. Модель: {Model}, длина сообщения: {MessageLength}.",
            selectedModel,
            message.Length);

        var response = await _llmProvider
            .GenerateAsync(selectedModel, message, cancellationToken)
            .ConfigureAwait(false);

        _logger.LogInformation(
            "Текстовый запрос к LLM успешно обработан. Модель: {Model}, длина ответа: {ResponseLength}.",
            selectedModel,
            response.Length);

        return response;
    }

    /// <summary>
    /// Выполняет структурированную PKI-классификацию запроса.
    /// </summary>
    /// <param name="model">Имя модели. Если не передано, используется модель по умолчанию.</param>
    /// <param name="message">Сообщение пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Структурированный результат классификации.</returns>
    public async Task<PkiIntentResult> ClassifyPkiIntentAsync(string? model, string message, CancellationToken cancellationToken = default)
    {
        var selectedModel = string.IsNullOrWhiteSpace(model) ? DefaultModel : model.Trim();

        _logger.LogInformation(
            "Начата PKI-классификация запроса. Модель: {Model}, длина сообщения: {MessageLength}.",
            selectedModel,
            message.Length);

        var response = await _llmProvider
            .ClassifyPkiIntentAsync(selectedModel, message, cancellationToken)
            .ConfigureAwait(false);

        _logger.LogInformation(
            "PKI-классификация завершена. Модель: {Model}, интент: {Intent}, confidence: {Confidence}.",
            selectedModel,
            response.Intent,
            response.Confidence);

        return response;
    }
}
