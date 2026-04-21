namespace Assistant.Web.Infrastructure.Clients.Models;

/// <summary>
/// Запрос к эндпоинту чата Ollama.
/// </summary>
public sealed class OllamaChatRequest
{
    /// <summary>
    /// Идентификатор модели.
    /// </summary>
    public string Model { get; init; } = string.Empty;

    /// <summary>
    /// Список сообщений диалога.
    /// </summary>
    public IReadOnlyCollection<OllamaChatMessageRequest> Messages { get; init; } = [];

    /// <summary>
    /// JSON Schema для структурированного ответа модели.
    /// </summary>
    public object? Format { get; init; }

    /// <summary>
    /// Дополнительные параметры генерации модели.
    /// </summary>
    public OllamaChatOptions? Options { get; init; }

    /// <summary>
    /// Отключает потоковый режим для получения цельного ответа.
    /// </summary>
    public bool Stream { get; init; } = false;
}
