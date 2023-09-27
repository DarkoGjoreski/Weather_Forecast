using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weather_Forecast.Application.Abstracitions;

namespace Weather_Forecast.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchByCityAsync([FromQuery] string city)
        {
            var result = await _weatherService.SearchByCity(city);
            return Ok(result);
        }
    }
}