namespace DecoratePattern.Models
{
    public class DomainEvent: IDomainEvent
    {
        public Guid Id { get; set; }

        public DateTime OccurredOn { get; set; }
    }
}
