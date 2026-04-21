namespace Assistant.Web.Application.Models;

/// <summary>
/// Структурированный результат маршрутизации запроса в домене PKI.
/// </summary>
public sealed class PkiIntentResult
{
    /// <summary>
    /// Определенный интент запроса.
    /// </summary>
    public string Intent { get; init; } = string.Empty;

    /// <summary>
    /// Тип сущности, найденной в запросе.
    /// </summary>
    public string EntityType { get; init; } = string.Empty;

    /// <summary>
    /// Значение сущности.
    /// </summary>
    public string EntityValue { get; init; } = string.Empty;

    /// <summary>
    /// Признак необходимости вызова инструмента.
    /// </summary>
    public bool ShouldUseTool { get; init; }

    /// <summary>
    /// Признак необходимости обращения к базе знаний.
    /// </summary>
    public bool ShouldUseKnowledgeBase { get; init; }

    /// <summary>
    /// Уверенность модели в классификации.
    /// </summary>
    public double Confidence { get; init; }

    /// <summary>
    /// Краткое обоснование решения модели.
    /// </summary>
    public string Reason { get; init; } = string.Empty;
}
