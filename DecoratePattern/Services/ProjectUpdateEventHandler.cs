namespace DecoratePattern.Services
{
    public class ProjectUpdateEventHandler 
    {
        public Task HandleAsync()
        {
            Console.WriteLine("ProjectUpdateEventHandler.HandleAsync");
            return Task.CompletedTask;
        }
    }
}
