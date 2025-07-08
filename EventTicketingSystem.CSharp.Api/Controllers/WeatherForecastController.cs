using EventTicketingSystem.CSharp.Domain.Features.BusinessOwner;
using EventTicketingSystem.CSharp.Domain.Models.Features.BusinessOwner;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.CSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly BL_BusinessOwner _businessOwnerService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, BL_BusinessOwner businessOwnerService)
        {
            _logger = logger;
            _businessOwnerService = businessOwnerService;
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

        [HttpPost]
        public IActionResult GetBusinessOwnerList([FromBody] BusinessOwnerRequestModel requestModel)
        {
            var result = _businessOwnerService.GetList(requestModel).Result;
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result.Message);
            }
        }
    }
}
