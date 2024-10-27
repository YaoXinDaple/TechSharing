using DecoratePattern.Models;
using DecoratePattern.Services;
using Microsoft.AspNetCore.Mvc;

namespace DecoratePattern.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IDomainEventHandler<ProjectCreateDomainEvent> _handler;


        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDomainEventHandler<ProjectCreateDomainEvent> handler)
        {
            _logger = logger;
            _handler = handler;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            ProjectCreateDomainEvent projectCreateDomainEvent = new ProjectCreateDomainEvent
            {
                Id = Guid.NewGuid(),
                OccurredOn = DateTime.Now
            };
            _handler.HandleAsync(projectCreateDomainEvent).Wait();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
