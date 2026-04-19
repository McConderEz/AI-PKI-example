# Dotnet-LLM-assitent

Локальный .NET Web API для работы с LLM через `Ollama`, с UI (`Open WebUI`) и централизованными логами (`Serilog + Seq`).

## Что делает проект

- Поднимает локальный LLM-стек в Docker:
- `Ollama` (инференс-модель).
- `Open WebUI` (веб-интерфейс для чата).
- `Seq` (просмотр логов приложения).
- Предоставляет HTTP API на ASP.NET Core:
- `POST /api/chat` — отправка сообщения в модель и получение ответа.
- Реализует слоистую архитектуру:
- `Application` — бизнес-логика.
- `Infrastructure` — интеграция с внешним LLM API через `Refit`.

## Технологический стек

- `.NET 10` (ASP.NET Core Web API)
- `Refit` (typed HTTP client)
- `Serilog` + `Serilog.Sinks.Seq` (структурированные логи)
- `Swagger / OpenAPI`
- `Docker Compose`
- `Ollama`, `Open WebUI`, `Seq`

## Структура проекта

```text
.
├─ compose.yaml
├─ .env.example
├─ scripts/
│  ├─ deploy.ps1
│  └─ deploy.sh
└─ src/
   └─ Assistant.Web/
      ├─ Application/
      │  ├─ Abstractions/
      │  │  └─ ILLMProvider.cs
      │  └─ Services/
      │     └─ LLMChatService.cs
      ├─ Infrastructure/
      │  ├─ Clients/
      │  │  ├─ ILLMClient.cs
      │  │  └─ Models/
      │  ├─ Providers/
      │  │  └─ LLMProvider.cs
      │  └─ Inject.cs
      ├─ Controllers/
      │  ├─ ChatController.cs
      │  └─ Models/
      ├─ Program.cs
      └─ appsettings*.json
```

## API

### `POST /api/chat`

Отправляет сообщение в LLM и возвращает текст ответа.

Пример запроса:

```json
{
  "model": "llama3.1:8b",
  "message": "Привет! Объясни, что такое RAG простыми словами."
}
```

Пример ответа:

```json
{
  "model": "llama3.1:8b",
  "message": "..."
}
```

### Swagger

- `http://localhost:8080/swagger`

## Логирование

- Приложение пишет логи через `Serilog`.
- Просмотр логов: `Seq` по адресу `http://localhost:8081`.
- Ingestion endpoint Seq: `http://localhost:5341`.

## Быстрый старт

### 1. Подготовка

Скопируйте переменные окружения:

```bash
cp .env.example .env
```

Для PowerShell:

```powershell
Copy-Item .env.example .env
```

### 2. Разворот через скрипт

PowerShell (Windows):

```powershell
.\scripts\deploy.ps1
```

Bash (Linux/macOS/WSL):

```bash
chmod +x ./scripts/deploy.sh
./scripts/deploy.sh
```

Скрипты:
- поднимают `llm`, `llm-init`, `open-webui`, `llm-seq`;
- собирают и поднимают `assistant.web`;
- выводят итоговые URL сервисов.

## Ручной запуск (без скриптов)

```bash
docker compose up -d llm llm-init open-webui llm-seq
docker compose up -d --build assistant.web
```

## Адреса сервисов после запуска

- API: `http://localhost:8080`
- Swagger: `http://localhost:8080/swagger`
- Ollama API: `http://localhost:11434`
- Open WebUI: `http://localhost:3000`
- Seq: `http://localhost:8081`

## Требования

- Docker Desktop + Docker Compose
- NVIDIA драйверы и поддержка GPU в Docker (для ускорения Ollama)
- .NET SDK 10 (для локальной сборки/запуска без Docker)
