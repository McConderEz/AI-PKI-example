using Assistant.Web.Application.Models;

namespace Assistant.Web.Application.Abstractions;

/// <summary>
/// Определяет абстракцию провайдера для работы с LLM.
/// </summary>
public interface ILLMProvider
{
    /// <summary>
    /// Генерирует текстовый ответ модели на основе входного сообщения.
    /// </summary>
    /// <param name="model">Имя модели.</param>
    /// <param name="message">Текст сообщения пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сгенерированный текст ответа.</returns>
    Task<string> GenerateAsync(string model, string message, CancellationToken cancellationToken = default);

    /// <summary>
    /// Выполняет структурированную PKI-классификацию запроса по заранее определенной схеме.
    /// </summary>
    /// <param name="model">Имя модели.</param>
    /// <param name="message">Текст сообщения пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Структурированный результат классификации.</returns>
    Task<PkiIntentResult> ClassifyPkiIntentAsync(string model, string message, CancellationToken cancellationToken = default);
}
