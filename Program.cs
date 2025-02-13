var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddApplicationInsightsTelemetry();
builder.Logging.AddApplicationInsights();
var app = builder.Build();

// Define a minimal API endpoint. The lambda receives an ILogger from DI.
app.MapGet("/", (ILogger<Program> logger) => {
    var yyyyMMdd_HHmmss = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    // Log an information-level message
    logger.LogInformation("{time} - Endpoint '/' was hit - returning hello world", yyyyMMdd_HHmmss);
    return $"hello world {yyyyMMdd_HHmmss}";
});

// get random number endpoint return number between 1000 and 9999
app.MapGet("/random", (ILogger<Program> logger) => {
    var random = new Random();
    var randomNumber = random.Next(1000, 9999);
    logger.LogInformation("{time} - Endpoint '/random' was hit - returning random number {randomNumber}", DateTime.Now.ToString("HH:mm:ss"), randomNumber);
    return randomNumber;
});

app.Run();
