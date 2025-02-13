var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Logging.AddApplicationInsights();

builder.Services.Configure<LoggerFilterOptions>(options => {
    var ruleToRemove = options.Rules.FirstOrDefault(
        rule => rule.ProviderName == "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider");

    if (ruleToRemove != null) {
        options.Rules.Remove(ruleToRemove);
    }
});

var app = builder.Build();

app.MapGet("/", (ILogger<Program> logger) => {
    var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    logger.LogInformation("{time} - Endpoint '/' was hit - returning hello world", timestamp);
    return $"hello world {timestamp}";
});


// get random number endpoint return number between 1000 and 9999
app.MapGet("/random", (ILogger<Program> logger) => {
    var random = new Random();
    var randomNumber = random.Next(1000, 9999);
    logger.LogInformation("{time} - Endpoint '/random' was hit - returning random number {randomNumber}", DateTime.Now.ToString("HH:mm:ss"), randomNumber);
    return randomNumber;
});

app.Run();
