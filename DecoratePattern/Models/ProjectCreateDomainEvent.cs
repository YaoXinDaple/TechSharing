
namespace DecoratePattern.Models
{
    public class ProjectCreateDomainEvent : IDomainEvent
    {
        public Guid Id { get; set; }
        public DateTime OccurredOn { get; set; } = DateTime.Now;
    }
}
