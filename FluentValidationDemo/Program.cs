using FluentValidation;
using FluentValidationDemo.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//自动注册程序集内的服务
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//手动注册验证服务
//builder.Services.AddTransient<IValidator<CreateDataRequest>, CreateDataRequestValidator>();

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
