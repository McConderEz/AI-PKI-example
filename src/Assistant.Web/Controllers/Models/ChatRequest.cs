namespace Assistant.Web.Controllers.Models;

/// <summary>
/// Входной запрос API для генерации ответа чата.
/// </summary>
public sealed class ChatRequest
{
    /// <summary>
    /// Имя модели. По умолчанию используется <c>llama3.1:8b</c>.
    /// </summary>
    public string Model { get; init; } = "llama3.1:8b";

    /// <summary>
    /// Сообщение пользователя.
    /// </summary>
    public string Message { get; init; } = string.Empty;
}
