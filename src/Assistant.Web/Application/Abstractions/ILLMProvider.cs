namespace Assistant.Web.Application.Abstractions;

/// <summary>
/// Определяет абстракцию провайдера для работы с LLM.
/// </summary>
public interface ILLMProvider
{
    /// <summary>
    /// Генерирует ответ модели на основе входного сообщения.
    /// </summary>
    /// <param name="model">Имя модели.</param>
    /// <param name="message">Текст сообщения пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сгенерированный текст ответа.</returns>
    Task<string> GenerateAsync(string model, string message, CancellationToken cancellationToken = default);
}
