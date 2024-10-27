namespace DecoratePattern.Models
{
    public interface IDomainEvent
    {
        public Guid Id { get; set; }

        public DateTime OccurredOn { get; set; }
    }
}
