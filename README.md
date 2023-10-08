# OpenTelemetry .Net Core Getting Started

Source: [Instrumentation with .Net getting started](https://opentelemetry.io/docs/instrumentation/net/getting-started/)

## Step by step

Create a new directory called `dotnet-simple` and `cd` it. Execute following command:

``` shell
dotnet new web
```

Replace the content of Program.cs with the following code:

``` csharp
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var logger = app.Logger;

int RollDice()
{
    return Random.Shared.Next(1, 7);
}

string HandleRollDice(string? player)
{
    var result = RollDice();

    if (string.IsNullOrEmpty(player))
    {
        logger.LogInformation("Anonymous player is rolling the dice: {result}", result);
    }
    else
    {
        logger.LogInformation("{player} is rolling the dice: {result}", player, result);
    }

    return result.ToString(CultureInfo.InvariantCulture);
}

app.MapGet("/rolldice/{player?}", HandleRollDice);

app.Run();
```

In the Properties subdirectory, replace the content of launchSettings.json with the following:

``` json
{
  "$schema": "http://json.schemastore.org/launchsettings.json",
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "rolldice",
      "applicationUrl": "http://localhost:8087",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

Build and run the application with the following command, then open http://localhost:8080/rolldice in your web browser to ensure it is working.

``` shell
dotnet build && dotnet run
```

Download installation scripts from Releases of the opentelemetry-dotnet-instrumentation repository:

``` shell
curl -L -O https://github.com/open-telemetry/opentelemetry-dotnet-instrumentation/releases/latest/download/otel-dotnet-auto-install.sh
```

Execute following script to download automatic instrumentation for your development environment:

``` shell
chmod +x ./otel-dotnet-auto-install.sh
./otel-dotnet-auto-install.sh
```

Set and export variables that specify a console exporter, then execute script configuring other necessary environment variables:

``` shell
export OTEL_TRACES_EXPORTER=none \
  OTEL_METRICS_EXPORTER=none \
  OTEL_LOGS_EXPORTER=none \
  OTEL_DOTNET_AUTO_TRACES_CONSOLE_EXPORTER_ENABLED=true \
  OTEL_DOTNET_AUTO_METRICS_CONSOLE_EXPORTER_ENABLED=true \
  OTEL_DOTNET_AUTO_LOGS_CONSOLE_EXPORTER_ENABLED=true
  OTEL_SERVICE_NAME=RollDiceService
. $HOME/.otel-dotnet-auto/instrument.sh
```

Run your application once again:

``` shell
dotnet run
```

From another terminal, send a request using curl:

``` shell
curl localhost:8087/rolldice
```

## Using instrumentation libraries

Source: [Instrumentation with .Net libraries](https://opentelemetry.io/docs/instrumentation/net/libraries/)

First, get the appropriate packages of OpenTelemetry Core:

``` shell
dotnet add package OpenTelemetry
dotnet add package OpenTelemetry.Extensions.Hosting
dotnet add package OpenTelemetry.Exporter.Console
```

Then you can install the Instrumentation packages:

``` shell
dotnet add package OpenTelemetry.Instrumentation.AspNetCore --prerelease
dotnet add package OpenTelemetry.Instrumentation.Http --prerelease
```

## Adding OpenTelemetry docker-compose

Implementation basen on [Monitoring metrics using otel](https://pratapsharma.com.np/monitoring-metrics-using-otel) article.

