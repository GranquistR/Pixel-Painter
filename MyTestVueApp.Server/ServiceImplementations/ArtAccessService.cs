using Microsoft.Extensions.Options;
using MyTestVueApp.Server.Configuration;
using MyTestVueApp.Server.Entities;
using MyTestVueApp.Server.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MyTestVueApp.Server.ServiceImplementations
{
    public class ArtAccessService : IArtAccessService
    {
        private IOptions<ApplicationConfiguration> AppConfig { get; }
        public ArtAccessService(IOptions<ApplicationConfiguration> appConfig)
        {
            AppConfig = appConfig;
        }
        public IEnumerable<Art> GetAllArt()
        {
            var paintings = new List<Art>();
            var connectionString = AppConfig.Value.ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //var query = "SELECT Date, TemperatureC, Summary FROM WeatherForecasts";
                var query = "SELECT ID, ArtName, Artistid, Width, ArtLength, Encode, CreationDate, isPublic FROM Art";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var painting = new Art
                            { //ArtId, ArtName, ArtistId, Width, ArtLength, Encode, Date, IsPublic
                                ArtId = reader.GetInt32(0),
                                ArtName = reader.GetString(1),
                                ArtistId = reader.GetInt32(2),
                                Width = reader.GetInt32(3),
                                ArtLength = reader.GetInt32(4),
                                Encode = reader.GetString(5),
                                CreationDate = reader.GetDateTime(6),
                                IsPublic = reader.GetBoolean(7)
                            };
                            paintings.Add(painting);
                        }
                    }
                }
            }

            return paintings;
        }

        public Art GetArtById(int id)
        {
            var connectionString = AppConfig.Value.ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //var query = "SELECT Date, TemperatureC, Summary FROM WeatherForecasts";
                var query = "SELECT ID, ArtName, Artistid, Width, ArtLength, Encode, CreationDate, isPublic FROM Art WHERE ID=" + id;
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read() != null)
                        {
                            var painting = new Art
                            { //ArtId, ArtName, ArtistId, Width, ArtLength, Encode, Date, IsPublic
                                ArtId = reader.GetInt32(0),
                                ArtName = reader.GetString(1),
                                ArtistId = reader.GetInt32(2),
                                Width = reader.GetInt32(3),
                                ArtLength = reader.GetInt32(4),
                                Encode = reader.GetString(5),
                                CreationDate = reader.GetDateTime(6),
                                IsPublic = reader.GetBoolean(7)
                            };
                            return painting;
                        }
                        return null;
                    }
                }
            }
        }
    }
}

