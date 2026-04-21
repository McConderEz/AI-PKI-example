namespace Assistant.Web.Infrastructure.Clients.Models;

/// <summary>
/// Опции генерации для запроса к чату Ollama.
/// </summary>
public sealed class OllamaChatOptions
{
    /// <summary>
    /// Температура генерации. Значение <c>0</c> повышает детерминированность ответа.
    /// </summary>
    public double Temperature { get; init; } = 0;
}
