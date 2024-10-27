using DecoratePattern.Models;

namespace DecoratePattern.Services
{
    public abstract class DomainEventHandler<TDomainEvent>: IDomainEventHandler<TDomainEvent> 
        where TDomainEvent : IDomainEvent
    {
        public abstract Task HandleAsync(TDomainEvent domainEvent);
        public Task HandleAsync(IDomainEvent domainEvent)
        {
            Console.WriteLine("DomainEventHandler.HandleAsync");
            return HandleAsync((TDomainEvent)domainEvent);
        }
    }
}
