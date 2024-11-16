using Microsoft.EntityFrameworkCore;
using OutboxExample.Contracts.Persistence;
using OutboxExampleAPI;
using Serilog.Events;
using Serilog;
using System.Reflection;
using OutboxExample.Contracts.Services;
using MassTransit;
using OutboxExample.Contracts.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("Default");
    opt.UseSqlServer(connectionString, cfg =>
    {
        cfg.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
        cfg.MigrationsHistoryTable($"__{nameof(AppDbContext)}");
    });
});
builder.Services.AddHostedService<DatabaseMigratorHostedService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddMassTransit(conf =>
{
    conf.AddConsumer(typeof(ReservationSubmittedConsumer));
    conf.AddEntityFrameworkOutbox<AppDbContext>(opt =>
    {
        opt.QueryDelay=TimeSpan.FromMinutes(1);
        opt.UseSqlServer();
        opt.UseBusOutbox();
    });
    conf.UsingInMemory((context, cfg) =>
    {
        //cfg.Host("localhost", h =>
        //{
        //    h.Username("guest");
        //    h.Password("guest");
        //});
        
        cfg.ConfigureEndpoints(context);
        cfg.AutoStart = true;
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
