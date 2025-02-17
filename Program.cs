using Azure.Monitor.OpenTelemetry.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddApplicationInsights();
builder.Logging.AddAzureWebAppDiagnostics();
builder.Services.AddOpenTelemetry().UseAzureMonitor();

// builder.Services.Configure<LoggerFilterOptions>(o => {
//     LoggerFilterRule toRemove = o.Rules.FirstOrDefault(rule => rule.ProviderName == "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider");
//     if (toRemove != null) { 
//         o.Rules.Remove(toRemove);
//     }
// });

var app = builder.Build();

app.MapGet("/", (ILogger<Program> logger) => {
    logger.LogInformation($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}, '/' - hello world");
    return "Hello World";
});


app.MapGet("/random", (ILogger<Program> logger) => {
    var random = new Random();
    var randomNumber = random.Next(1000, 9999);
    logger.LogInformation($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}, '/random' - number: {randomNumber}");
    return randomNumber;
});

app.Run();
