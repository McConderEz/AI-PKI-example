using Assistant.Web.Application.Abstractions;
using Assistant.Web.Infrastructure.Clients;
using Microsoft.Extensions.Logging;

namespace Assistant.Web.Infrastructure.Providers;

/// <summary>
/// Реализация провайдера LLM уровня инфраструктуры.
/// </summary>
public sealed class LLMProvider : ILLMProvider
{
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
    /// Выполняет генерацию ответа через API Ollama.
    /// </summary>
    /// <param name="model">Имя модели.</param>
    /// <param name="message">Сообщение пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сгенерированный ответ модели.</returns>
    public async Task<string> GenerateAsync(string model, string message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Начат вызов внешнего LLM API. Модель: {Model}, длина сообщения: {MessageLength}.",
            model,
            message.Length);

        try
        {
            var response = await _llmClient.ChatAsync(model, message, cancellationToken).ConfigureAwait(false);
            var content = response.Message?.Content ?? string.Empty;

            _logger.LogInformation(
                "Вызов внешнего LLM API завершен успешно. Модель: {Model}, длина ответа: {ResponseLength}.",
                model,
                content.Length);

            return content;
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Ошибка при вызове внешнего LLM API. Модель: {Model}.",
                model);

            throw;
        }
    }
}
