using FluentValidation;
using FluentValidationDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidationDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IValidator<CreateDataRequest> _createDataRequestValidator;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IValidator<CreateDataRequest> createDataRequestValidator)
        {
            _logger = logger;
            _createDataRequestValidator = createDataRequestValidator;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("use-validator-constructor")]
        public IActionResult PostUseValidatorConstructor([FromBody] CreateDataRequest request)
        {
            var validator = new CreateDataRequestValidator();
            var result = validator.Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [HttpPost("use-validator-di")]
        public IActionResult PostUseValidatorDi([FromBody] CreateDataRequest request)
        {
            var result = _createDataRequestValidator.Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }
    }
}
