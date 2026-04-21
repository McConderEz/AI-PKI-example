using Assistant.Web.Application.Models;
using Assistant.Web.Application.Services;
using Assistant.Web.Controllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assistant.Web.Controllers;

/// <summary>
/// Контроллер для работы с LLM-чатом.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class ChatController : ControllerBase
{
    private readonly LLMChatService _chatService;
    private readonly ILogger<ChatController> _logger;

    /// <summary>
    /// Инициализирует экземпляр контроллера чата.
    /// </summary>
    /// <param name="chatService">Сервис чата прикладного слоя.</param>
    /// <param name="logger">Логгер контроллера.</param>
    public ChatController(LLMChatService chatService, ILogger<ChatController> logger)
    {
        _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Генерирует текстовый ответ LLM по входному сообщению.
    /// </summary>
    /// <param name="request">Запрос, содержащий модель и сообщение.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сгенерированный текстовый ответ модели.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ChatResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ChatResponse>> Post([FromBody] ChatRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            _logger.LogWarning("Отклонен запрос к API чата: пустое сообщение.");
            return BadRequest("Параметр message обязателен.");
        }

        var selectedModel = string.IsNullOrWhiteSpace(request.Model)
            ? LLMChatService.DefaultModel
            : request.Model.Trim();

        _logger.LogInformation(
            "Получен запрос к API чата. Модель: {Model}, длина сообщения: {MessageLength}.",
            selectedModel,
            request.Message.Length);

        var responseMessage = await _chatService
            .ChatAsync(selectedModel, request.Message, cancellationToken)
            .ConfigureAwait(false);

        _logger.LogInformation(
            "Запрос к API чата выполнен успешно. Модель: {Model}, длина ответа: {ResponseLength}.",
            selectedModel,
            responseMessage.Length);

        return Ok(new ChatResponse
        {
            Model = selectedModel,
            Message = responseMessage
        });
    }

    /// <summary>
    /// Выполняет structured PKI-классификацию запроса.
    /// </summary>
    /// <param name="request">Запрос, содержащий модель и текст пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Структурированный результат классификации PKI-роутера.</returns>
    [HttpPost("pki-router")]
    [ProducesResponseType(typeof(PkiIntentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PkiIntentResult>> PkiRouter([FromBody] PkiIntentRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            _logger.LogWarning("Отклонен запрос PKI-роутера: пустое сообщение.");
            return BadRequest("Параметр message обязателен.");
        }

        var selectedModel = string.IsNullOrWhiteSpace(request.Model)
            ? LLMChatService.DefaultModel
            : request.Model.Trim();

        _logger.LogInformation(
            "Получен запрос к PKI-роутеру. Модель: {Model}, длина сообщения: {MessageLength}.",
            selectedModel,
            request.Message.Length);

        var result = await _chatService
            .ClassifyPkiIntentAsync(selectedModel, request.Message, cancellationToken)
            .ConfigureAwait(false);

        _logger.LogInformation(
            "Запрос к PKI-роутеру выполнен успешно. Модель: {Model}, интент: {Intent}, confidence: {Confidence}.",
            selectedModel,
            result.Intent,
            result.Confidence);

        return Ok(result);
    }
}
