using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddFusionCache()
    .WithDefaultEntryOptions(options=>options.Duration = TimeSpan.FromMinutes(5))
    .WithSerializer(new FusionCacheSystemTextJsonSerializer())
    .WithDistributedCache(
        new RedisCache(new RedisCacheOptions { Configuration = "localhost:6379"})
    );

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

app.MapGet("/weather/{city}",
    async (string city, IFusionCache fusionCache) =>
{
    var forecast = await fusionCache.GetOrSetAsync(
        $"weather-{city}",
        async () =>
        {
            // Simulate a long-running operation
            await Task.Delay(1000);
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast(
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                rng.Next(-20, 55),
                summaries[rng.Next(summaries.Length)]))
                .ToArray();
        }, tags: ["weather"]);
    return forecast;
});

app.MapPost("/weather/clear",
    async (string tagName,IFusionCache fusionCache)=>
    {
        if (string.IsNullOrWhiteSpace(tagName))
        {
            return;
        }
        await fusionCache.RemoveByTagAsync(tagName);
    }
);

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
