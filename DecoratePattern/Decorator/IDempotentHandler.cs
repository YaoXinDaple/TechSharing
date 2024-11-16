using DecoratePattern.Models;
using DecoratePattern.Services;

namespace DecoratePattern.Decorator
{
    public class IDempotentHandler<TDomainEvent>(IDomainEventHandler<TDomainEvent> domainEventHandler)
        :DomainEventHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        public override Task HandleAsync(TDomainEvent domainEvent)
        {
            Console.WriteLine("Start dempotent!");

            domainEventHandler.HandleAsync(domainEvent);

            Console.WriteLine("End dempotent!");

            return Task.CompletedTask;
        }
    }
}
