using Microsoft.Extensions.Logging.AzureAppServices;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddApplicationInsights();
builder.Logging.AddAzureWebAppDiagnostics();

// builder.Services.AddApplicationInsightsTelemetry(o => o.EnableAdaptiveSampling = false);
// builder.Services.Configure<AzureFileLoggerOptions>(builder.Configuration.GetSection("AzureLogging"));

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
