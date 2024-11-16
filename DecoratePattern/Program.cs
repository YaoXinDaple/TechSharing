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
    // 检查是否实现了 IDomainEventHandler<T> 接口
    var handlerInterface = domainEventHandler.GetInterfaces()
        .FirstOrDefault(x =>
            x.IsGenericType &&
            x.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>));

    if (handlerInterface != null)
    {
        // 注册具体的处理程序
        builder.Services.TryAddScoped(handlerInterface, domainEventHandler);

        // 获取泛型参数类型数组
        Type domainEvent = handlerInterface.GetGenericArguments()[0];

        // 创建装饰器类型
        Type idempotentHandler = typeof(IDempotentHandler<>).MakeGenericType(domainEvent);

        // 注册装饰器
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
