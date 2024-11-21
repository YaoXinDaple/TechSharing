using AspireWithResilientHttpClient.ApiService.Services;
using Microsoft.Extensions.Logging;
using Polly;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddScoped<LemonInvoiceSuiteService>();

builder.Services.AddHttpClient<LemonInvoiceSuiteService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5000");
})
//.AddStandardResilienceHandler();
.AddResilienceHandler("pipeline", pipeline =>
{
    pipeline.AddTimeout(TimeSpan.FromSeconds(5));

    pipeline.AddRetry(new Polly.Retry.RetryStrategyOptions<HttpResponseMessage>
    {
        MaxRetryAttempts = 3,
        BackoffType = DelayBackoffType.Exponential,
        UseJitter = true,
        Delay = TimeSpan.FromMilliseconds(500)
    });
    pipeline.AddTimeout(TimeSpan.FromSeconds(1));

    //pipeline.AddCircuitBreaker(new Polly.CircuitBreaker.CircuitBreakerStrategyOptions<HttpResponseMessage>
    //{
    //    SamplingDuration = TimeSpan.FromSeconds(10),
    //    FailureRatio = 0.9,
    //    MinimumThroughput = 5,
    //    BreakDuration = TimeSpan.FromSeconds(30)
    //});
})
;

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.MapGet("/loop", async (LemonInvoiceSuiteService service,ILogger<Program> logger) =>
{
    while (true)
    {
        await service.ReadDataAsync();
    }
})
.WithName("loop");

app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
