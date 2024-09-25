using Microsoft.AspNetCore.Mvc;
using MyTestVueApp.Server.Entities;
using MyTestVueApp.Server.Interfaces;

namespace MyTestVueApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ILogger<WeatherForecastController> Logger { get; }
        private IWeatherForecastService WeatherForecastService { get; }

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
        {
            Logger = logger;
            WeatherForecastService = weatherForecastService;
        }

        [HttpGet]
        [Route("GetWeatherForecast")]
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
           return WeatherForecastService.GetWeatherForecast();
        }
    }
}
