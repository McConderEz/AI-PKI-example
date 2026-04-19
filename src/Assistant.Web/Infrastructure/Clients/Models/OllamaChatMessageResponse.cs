namespace Assistant.Web.Infrastructure.Clients.Models;

/// <summary>
/// Сообщение ассистента в ответе Ollama.
/// </summary>
public sealed class OllamaChatMessageResponse
{
    /// <summary>
    /// Роль участника диалога.
    /// </summary>
    public string Role { get; init; } = string.Empty;

    /// <summary>
    /// Текст сообщения.
    /// </summary>
    public string Content { get; init; } = string.Empty;
}
