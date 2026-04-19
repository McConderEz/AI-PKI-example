param(
    [switch]$Rebuild
)

$ErrorActionPreference = "Stop"

Write-Host "==> Проверка Docker Compose..."
docker compose version | Out-Null

if (-not (Test-Path ".env")) {
    if (Test-Path ".env.example") {
        Copy-Item ".env.example" ".env"
        Write-Host "Создан .env из .env.example"
    }
}

Write-Host "==> Поднимаем LLM-инфраструктуру (Ollama, Open WebUI, Seq)..."
docker compose up -d llm llm-init open-webui llm-seq

if ($Rebuild) {
    Write-Host "==> Пересобираем и поднимаем assistant.web..."
    docker compose up -d --build assistant.web
}
else {
    Write-Host "==> Поднимаем assistant.web..."
    docker compose up -d assistant.web
}

Write-Host "==> Текущий статус контейнеров:"
docker compose ps

Write-Host ""
Write-Host "Готово."
Write-Host "API:        http://localhost:8080"
Write-Host "Swagger:    http://localhost:8080/swagger"
Write-Host "Open WebUI: http://localhost:3000"
Write-Host "Seq:        http://localhost:8081"
