var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddApplicationInsights();

builder.Services.AddApplicationInsightsTelemetry();

// builder.Services.Configure<LoggerFilterOptions>(options => {
//     var ruleToRemove = options.Rules.FirstOrDefault(
//         rule => rule.ProviderName == "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider");

//     if (ruleToRemove != null) {
//         options.Rules.Remove(ruleToRemove);
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
