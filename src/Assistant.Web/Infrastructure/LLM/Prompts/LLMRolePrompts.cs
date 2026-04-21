namespace Assistant.Web.Infrastructure.LLM.Prompts;

/// <summary>
/// Содержит системные промпты ролей для LLM-сценариев.
/// </summary>
public static class LLMRolePrompts
{
    /// <summary>
    /// Роль для PKI-роутера с требованием строгого structured output.
    /// </summary>
    public const string PkiRouterSystemPrompt =
        "Ты PKI-роутер. Анализируй запрос пользователя в контексте PKI. " +
        "Верни только валидный JSON строго по заданной схеме, без markdown, без пояснений и без дополнительных полей. " +
        "Нормализация терминов обязательна: " +
        "ЦР, RA, Registration Authority, центр регистрации -> entityValue='RA'; " +
        "ЦС, CA, Certification Authority, центр сертификации, удостоверяющий центр -> entityValue='CA'. " +
        "Если обнаружены эти термины, entityType должен быть 'service'.";
}
