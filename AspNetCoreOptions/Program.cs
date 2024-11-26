using AspNetCoreOptions.Options;
using AspNetCoreOptions.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient<FirstService>();
builder.Services.AddTransient<SecondService>();
builder.Services.AddTransient<ThirdService>();
builder.Services.AddTransient<FourthService>();

/*
注入选项类
*/
builder.Services.AddOptions<AlphaOption>()
    .BindConfiguration("Alpha")
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<BetaOption>()
    .BindConfiguration("Beta")
    .ValidateDataAnnotations()
    .ValidateOnStart();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapGet("/original", (
    IConfiguration confgiration) =>
{
    AlphaOption alphaOption = confgiration.GetSection("Alpha").Get<AlphaOption>();
    BetaOption betaOption = confgiration.GetSection("Beta").Get<BetaOption>();

    Console.WriteLine($"Alpha: {alphaOption.Name}, {alphaOption.Identifier}");
    Console.WriteLine($"Beta: {betaOption.Count}, {betaOption.Interval}");


    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    return forecast;
})
.WithName("original")
.WithOpenApi();

app.MapGet("/optimized", async (
    FirstService firstService,
    SecondService secondService,
    ThirdService thirdService,
    FourthService fourthService) =>
{
    thirdService.DoWork();


    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    await Task.Delay(15000);

    thirdService.DoWork();
    return forecast;
})
.WithName("optimized")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
