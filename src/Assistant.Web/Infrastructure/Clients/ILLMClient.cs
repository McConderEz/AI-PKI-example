using Assistant.Web.Infrastructure.Clients.Models;
using Refit;

namespace Assistant.Web.Infrastructure.Clients;

/// <summary>
/// Refit-клиент для HTTP-взаимодействия с API LLM.
/// </summary>
public interface ILLMClient
{
    /// <summary>
    /// Выполняет запрос к эндпоинту чата Ollama.
    /// </summary>
    /// <param name="request">Тело запроса к модели.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Ответ модели в формате Ollama.</returns>
    [Post("/api/chat")]
    Task<OllamaChatResponse> ChatRawAsync([Body] OllamaChatRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Выполняет запрос к эндпоинту чата Ollama с указанием модели и сообщения.
    /// </summary>
    /// <param name="model">Имя модели. По умолчанию <c>llama3.1:8b</c>.</param>
    /// <param name="message">Сообщение пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Ответ модели в формате Ollama.</returns>
    public async Task<OllamaChatResponse> ChatAsync(
        string model = "llama3.1:8b",
        string message = "",
        CancellationToken cancellationToken = default)
    {
        var request = new OllamaChatRequest
        {
            Model = model,
            Stream = false,
            Messages =
            [
                new OllamaChatMessageRequest
                {
                    Role = "user",
                    Content = message
                }
            ],
            Options = new OllamaChatOptions
            {
                Temperature = 0
            }
        };

        return await ChatRawAsync(request, cancellationToken).ConfigureAwait(false);
    }
}
