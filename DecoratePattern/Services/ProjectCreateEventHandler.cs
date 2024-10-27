
using DecoratePattern.Models;

namespace DecoratePattern.Services
{
    public class ProjectCreateEventHandler : DomainEventHandler<ProjectCreateDomainEvent>
    {
        public override Task HandleAsync(ProjectCreateDomainEvent domainEvent)
        {
            Console.WriteLine("ProjectEventHadndler.HandleAsync");
            return Task.CompletedTask;
        }
    }
}
