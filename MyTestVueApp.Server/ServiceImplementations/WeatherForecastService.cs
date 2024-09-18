using Microsoft.Extensions.Options;
using MyTestVueApp.Server.Configuration;
using MyTestVueApp.Server.Entities;
using MyTestVueApp.Server.Interfaces;
using Microsoft.Data.SqlClient;

namespace MyTestVueApp.Server.ServiceImplementations
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private IOptions<ApplicationConfiguration> AppConfig { get; }
        public WeatherForecastService(IOptions<ApplicationConfiguration> appConfig)
        {
            AppConfig = appConfig;
        }
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            var forecasts = new List<WeatherForecast>();
            var connectionString = AppConfig.Value.ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT Date, TemperatureC, Summary FROM WeatherForecasts";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var forecast = new WeatherForecast
                            {
                                Date = reader.GetDateTime(0),
                                TemperatureC = reader.GetInt32(1),
                                Summary = reader.GetString(2)
                            };
                            forecasts.Add(forecast);
                        }
                    }
                }
            }

            return forecasts;
        }
    }
}
