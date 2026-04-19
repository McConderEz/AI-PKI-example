#!/usr/bin/env bash
set -euo pipefail

REBUILD="${1:-}"

echo "==> Проверка Docker Compose..."
docker compose version >/dev/null

if [[ ! -f ".env" && -f ".env.example" ]]; then
  cp ".env.example" ".env"
  echo "Создан .env из .env.example"
fi

echo "==> Поднимаем LLM-инфраструктуру (Ollama, Open WebUI, Seq)..."
docker compose up -d llm llm-init open-webui llm-seq

if [[ "$REBUILD" == "--rebuild" ]]; then
  echo "==> Пересобираем и поднимаем assistant.web..."
  docker compose up -d --build assistant.web
else
  echo "==> Поднимаем assistant.web..."
  docker compose up -d assistant.web
fi

echo "==> Текущий статус контейнеров:"
docker compose ps

echo
echo "Готово."
echo "API:        http://localhost:8080"
echo "Swagger:    http://localhost:8080/swagger"
echo "Open WebUI: http://localhost:3000"
echo "Seq:        http://localhost:8081"
