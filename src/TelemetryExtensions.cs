
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

public static class TelemetryExtensions
{
  public static void ConfigureLogging(this WebApplicationBuilder builder)
  {
    var serviceName = builder.Configuration["OpenTelemetry:Exporters:Zipkin:ServiceName"] ?? builder.Environment.ApplicationName;
    builder.Logging.ClearProviders();

    builder.Logging.AddOpenTelemetry(logging =>
    {
      logging.IncludeScopes = true;

      var resourceBuilder = ResourceBuilder
          .CreateDefault()
          .AddService(serviceName);

      logging.SetResourceBuilder(resourceBuilder)
            .AddConsoleExporter(); // ConsoleExporter is used for demo purpose only.
    });

  }

  public static void ConfigureOpenTelemetry(this WebApplicationBuilder builder)
  {
    builder.Services.AddOpenTelemetry()
           .WithTracing(b =>
           {
             b
             .AddHttpClientInstrumentation()
             .AddAspNetCoreInstrumentation()
             ;
           })
           .WithMetrics(b =>
           {
              b
              .AddHttpClientInstrumentation()
              .AddAspNetCoreInstrumentation();
           });
  }
}
