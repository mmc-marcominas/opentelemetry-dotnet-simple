#!/bin/sh
set -e

export OTEL_TRACES_EXPORTER=none \
  OTEL_METRICS_EXPORTER=none \
  OTEL_LOGS_EXPORTER=none \
  OTEL_DOTNET_AUTO_TRACES_CONSOLE_EXPORTER_ENABLED=true \
  OTEL_DOTNET_AUTO_METRICS_CONSOLE_EXPORTER_ENABLED=true \
  OTEL_DOTNET_AUTO_LOGS_CONSOLE_EXPORTER_ENABLED=true
  OTEL_SERVICE_NAME=RollDiceService
. $HOME/.otel-dotnet-auto/instrument.sh

dotnet clean
dotnet build --configuration Release
dotnet publish --configuration Release
tree . -I 'test*|obj|bin'
dotnet run
