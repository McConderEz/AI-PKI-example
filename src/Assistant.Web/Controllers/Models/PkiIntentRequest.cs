namespace Assistant.Web.Controllers.Models;

/// <summary>
/// Входной запрос API для PKI-классификации.
/// </summary>
public sealed class PkiIntentRequest
{
    /// <summary>
    /// Имя модели. По умолчанию используется <c>llama3.1:8b</c>.
    /// </summary>
    public string Model { get; init; } = "llama3.1:8b";

    /// <summary>
    /// Текст запроса пользователя.
    /// </summary>
    public string Message { get; init; } = string.Empty;
}
