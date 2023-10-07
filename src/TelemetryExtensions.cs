using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

public static class TelemetryExtensions
{
  public static void ConfigureLogging(this WebApplicationBuilder builder)
  {
    builder.Logging.ClearProviders();

    builder.Logging.AddOpenTelemetry(logging =>
    {
      logging.IncludeScopes = true;

      var resourceBuilder = ResourceBuilder
          .CreateDefault()
          .AddService(builder.Environment.ApplicationName);

      logging.SetResourceBuilder(resourceBuilder)
            .AddConsoleExporter(); // ConsoleExporter is used for demo purpose only.
    });

  }

  public static void ConfigureTracing(this WebApplicationBuilder builder)
  {
    builder.Services.AddOpenTelemetry()
           .WithTracing(b =>
           {
             b
             .AddHttpClientInstrumentation()
             .AddAspNetCoreInstrumentation();
           });
  }

  public static void ConfigureMetrics(this WebApplicationBuilder builder)
  {
    builder.Services.AddOpenTelemetry()
           .WithMetrics(b =>
           {
              b
              .AddHttpClientInstrumentation()
              .AddAspNetCoreInstrumentation();
           });
  }
}
