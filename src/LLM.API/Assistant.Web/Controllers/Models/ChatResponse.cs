namespace Assistant.Web.Controllers.Models;

/// <summary>
/// Ответ API с результатом генерации чата.
/// </summary>
public sealed class ChatResponse
{
    /// <summary>
    /// Имя модели, которая использована для генерации.
    /// </summary>
    public string Model { get; init; } = string.Empty;

    /// <summary>
    /// Ответ модели.
    /// </summary>
    public string Message { get; init; } = string.Empty;
}
