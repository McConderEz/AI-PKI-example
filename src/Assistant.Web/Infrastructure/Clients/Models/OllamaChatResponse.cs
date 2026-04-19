namespace Assistant.Web.Infrastructure.Clients.Models;

/// <summary>
/// Ответ Ollama от эндпоинта чата.
/// </summary>
public sealed class OllamaChatResponse
{
    /// <summary>
    /// Сообщение ассистента.
    /// </summary>
    public OllamaChatMessageResponse? Message { get; init; }
}
