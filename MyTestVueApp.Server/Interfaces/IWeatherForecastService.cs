using MyTestVueApp.Server.Entities;

namespace MyTestVueApp.Server.Interfaces
{
    /// <summary>
    /// Interface defines the SQL service.
    /// </summary>
    public interface IWeatherForecastService
    {
        /// <summary>
        /// Get the weather forecast.
        /// </summary>
        /// <returns>The weather forecast.</returns>
        public IEnumerable<WeatherForecast> GetWeatherForecast();
    }
}
