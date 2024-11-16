using DecoratePattern.Models;

namespace DecoratePattern.Services
{
    public interface IDomainEventHandler
    {
        public Task HandleAsync(IDomainEvent domainEvent);
    }

    public interface IDomainEventHandler<in TDomainEvent> : IDomainEventHandler
        where TDomainEvent : IDomainEvent
    {
        public Task HandleAsync(TDomainEvent domainEvent);
    }
}
