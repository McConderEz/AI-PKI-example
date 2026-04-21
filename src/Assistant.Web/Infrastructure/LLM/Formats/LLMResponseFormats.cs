using System.Text.Json;

namespace Assistant.Web.Infrastructure.LLM.Formats;

/// <summary>
/// Содержит предопределенные JSON Schema форматы ответов для LLM.
/// </summary>
public static class LLMResponseFormats
{
    /// <summary>
    /// Формат structured output для PKI-роутера.
    /// </summary>
    public static JsonElement PkiRouterIntentFormat { get; } = JsonDocument.Parse(
        """
        {
          "type": "object",
          "properties": {
            "intent": {
              "type": "string",
              "enum": [
                "general_question",
                "service_status",
                "certificate_lookup",
                "incident_lookup",
                "documentation_query"
              ]
            },
            "entityType": {
              "type": "string",
              "enum": [
                "none",
                "service",
                "certificate",
                "incident",
                "document_topic"
              ]
            },
            "entityValue": {
              "type": "string",
              "enum": [
                "none",
                "CA",
                "RA",
                "CertRegistry",
                "OCSPChecker"
              ]
            },
            "shouldUseTool": {
              "type": "boolean"
            },
            "shouldUseKnowledgeBase": {
              "type": "boolean"
            },
            "confidence": {
              "type": "number"
            },
            "reason": {
              "type": "string"
            }
          },
          "required": [
            "intent",
            "entityType",
            "entityValue",
            "shouldUseTool",
            "shouldUseKnowledgeBase",
            "confidence",
            "reason"
          ]
        }
        """).RootElement.Clone();
}
