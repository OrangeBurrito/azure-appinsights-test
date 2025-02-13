var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


var app = builder.Build();

// Define a minimal API endpoint. The lambda receives an ILogger from DI.
app.MapGet("/", (ILogger<Program> logger) =>
{
    // Log an information-level message
    logger.LogInformation("Endpoint '/' was hit - returning hello world");
    return "hello world";
});

app.Run();
