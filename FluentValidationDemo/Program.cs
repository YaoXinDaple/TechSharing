using FluentValidation;
using FluentValidationDemo.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//�Զ�ע������ڵķ���
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//�ֶ�ע����֤����
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