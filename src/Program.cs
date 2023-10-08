using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureLogging();

builder.ConfigureOpenTelemetry();

builder.Services.AddEndpointsApiExplorer();

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

    return $"{result.ToString(CultureInfo.InvariantCulture)}{Environment.NewLine}";
}

app.MapGet("/rolldice/{player?}", HandleRollDice);

var httpClient = new HttpClient();

async Task<string> HandleHello()
{
    var html = await httpClient.GetStringAsync("https://example.com/");

    if (string.IsNullOrEmpty(html))
    {
        var paragraph = $"<p>The time on the server is {DateTime.Now:O}</p>";
        var body = $"<h1>Hello World</h1>{paragraph}";
        var head = "<title>miniHTML</title>";
        var result = $"<html><head>{head}</head><body>{body}</body></html>";

        logger.LogInformation("Failure getting https://example.com - returning default html", result);
        return result;
    }

    logger.LogInformation("Returning https://example.com content", html);

    return html;
}

app.MapGet("/hello", async context =>
{
    context.Response.Headers.Add("Content-Type", "text/html");
    var response = await HandleHello();
    await context.Response.WriteAsync(response);
});

app.Run();
