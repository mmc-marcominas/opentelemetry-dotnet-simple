#!/bin/sh
set -e

export OTEL_TRACES_EXPORTER=otlp \
  OTEL_METRICS_EXPORTER=prometheus \
  OTEL_LOGS_EXPORTER=otlp \
  OTEL_DOTNET_AUTO_TRACES_CONSOLE_EXPORTER_ENABLED=true \
  OTEL_DOTNET_AUTO_METRICS_CONSOLE_EXPORTER_ENABLED=true \
  OTEL_DOTNET_AUTO_LOGS_CONSOLE_EXPORTER_ENABLED=true \
  OTEL_SERVICE_NAME=Open-Telemetry-Example
. $HOME/.otel-dotnet-auto/instrument.sh

dotnet clean
dotnet build --configuration Release
dotnet publish --configuration Release
tree . -I 'test*|obj|bin'
dotnet run
