namespace Assistant.Web.Infrastructure.Clients.Models;

/// <summary>
/// Сообщение пользователя или ассистента в запросе к Ollama.
/// </summary>
public sealed class OllamaChatMessageRequest
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
