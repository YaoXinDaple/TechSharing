using DddWithEntityCache.Domain;
using DddWithEntityCache.Dto;
using DddWithEntityCache.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using static DddWithEntityCache.Domain.Invoice;
using static DddWithEntityCache.Domain.InvoiceEntry;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<InvoiceDbContext>(options => { 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<InvoiceFactory>();
builder.Services.AddScoped<InvoiceEntryFactory>();
builder.Services.AddFusionCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

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
})
.WithName("GetWeatherForecast");

app.MapPost("/invoice", async (
    [FromBody]InvoiceDto dto, 
    [FromServices] IInvoiceRepository invoiceRepository, 
    [FromServices] InvoiceFactory invoiceFactory, 
    [FromServices] InvoiceEntryFactory invoiceEntryFactory ) =>
{
    var entries = new List<InvoiceEntry>();

    dto.Entries.ForEach(entryDto =>
    {
        entries.Add(invoiceEntryFactory.Create(Guid.CreateVersion7(), entryDto.ProductName, entryDto.Price, entryDto.Quantity));
    });

    Invoice invoice = invoiceFactory.Create(Guid.CreateVersion7(), dto.CustomerName, dto.SellerName, dto.Amount, entries);
    await invoiceRepository.InsertAsync(invoice);
    return Results.Created($"/invoice?id={invoice.Id}", invoice);
});

app.MapGet("/invoice",async ([FromQuery] Guid id, [FromServices] IInvoiceRepository invoiceRepository) =>
{
    var invoice = await invoiceRepository.GetAsync(id);
    if (invoice == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(invoice);
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
