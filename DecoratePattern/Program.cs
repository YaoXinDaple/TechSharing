using DecoratePattern.Decorator;
using DecoratePattern.Models;
using DecoratePattern.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


Type[] domainEventHandlers = typeof(Program).Assembly.GetTypes()
    .Where(t => 
        t.IsAssignableTo(typeof(IDomainEventHandler)) &&
        !t.IsInterface && 
        !t.IsAbstract && 
        !t.IsGenericType)
    .ToArray();

foreach (var domainEventHandler in domainEventHandlers)
{
    // ����Ƿ�ʵ���� IDomainEventHandler<T> �ӿ�
    var handlerInterface = domainEventHandler.GetInterfaces()
        .FirstOrDefault(x =>
            x.IsGenericType &&
            x.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>));

    if (handlerInterface != null)
    {
        // ע�����Ĵ������
        builder.Services.TryAddScoped(handlerInterface, domainEventHandler);

        // ��ȡ���Ͳ�����������
        Type domainEvent = handlerInterface.GetGenericArguments()[0];

        // ����װ��������
        Type idempotentHandler = typeof(IDempotentHandler<>).MakeGenericType(domainEvent);

        // ע��װ����
        builder.Services.Decorate(handlerInterface, idempotentHandler);
    }
}



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
