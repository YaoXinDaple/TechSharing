using RateLimiterExp.RateLimiter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IRateLimiter, TokenBucketLimiter>();
builder.Services.AddSingleton<IAsyncRateLimiter, TokenBucketLimiterAsynchronous>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", async (IRateLimiter rateLimiter,IAsyncRateLimiter asyncRateLimiter) =>
{
    //for (int i = 0; i < 100; i++)
    //{
    //    Console.WriteLine(rateLimiter.Acquire());
    //    Thread.Sleep(333);
    //}


    for (int i = 0; i < 100; i++)
    {
        Console.WriteLine( await asyncRateLimiter.AcquireAsync());
        Thread.Sleep(333);
    }
})
.WithName("GetWeatherForecast");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
