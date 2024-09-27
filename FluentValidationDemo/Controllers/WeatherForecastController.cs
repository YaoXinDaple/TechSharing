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
        private readonly IValidator<UpdateDataRequest> _updateDataRequestValidator;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IValidator<CreateDataRequest> createDataRequestValidator,
            IValidator<UpdateDataRequest> updateDataRequestValidator)
        {
            _logger = logger;
            _createDataRequestValidator = createDataRequestValidator;
            _updateDataRequestValidator = updateDataRequestValidator;
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
        public async Task<IActionResult> PostUseValidatorConstructor([FromBody] CreateDataRequest request)
        {
            //var validator = new CreateDataRequestValidator();
            var result = await _createDataRequestValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [HttpPost("use-validator-di")]
        public async Task<IActionResult> PostUseValidatorDi([FromBody] CreateDataRequest request)
        {
            var result = await _createDataRequestValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [HttpPost("post-use-async-validator")]
        public async Task<IActionResult> PostUseAsynchronousValidate([FromBody] UpdateDataRequest request)
        {
            var result = await _updateDataRequestValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }
    }
}
