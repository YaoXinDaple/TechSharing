using Elastic.Clients.Elasticsearch;
using ElasticSearchApi.Configuration;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add Elasticsearch configuration
builder.Services.Configure<ElasticsearchSettings>(
    builder.Configuration.GetSection("Elasticsearch"));

// Add Elasticsearch client
builder.Services.AddSingleton<ElasticsearchClient>(serviceProvider =>
{
    var config = builder.Configuration.GetSection("Elasticsearch").Get<ElasticsearchSettings>();
    var settings = new ElasticsearchClientSettings(new Uri(config!.Uri));
    return new ElasticsearchClient(settings);
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
