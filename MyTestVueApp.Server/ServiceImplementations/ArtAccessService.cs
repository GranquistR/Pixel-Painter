using Microsoft.Extensions.Options;
using MyTestVueApp.Server.Configuration;
using MyTestVueApp.Server.Entities;
using MyTestVueApp.Server.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using Microsoft.IdentityModel.Tokens;

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
                var query = "SELECT ID, ArtName, ArtistId, ArtistName, Width, ArtLength, Encode, CreationDate, isPublic FROM Art";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var painting = new Art
                            { //ArtId, ArtName, ArtistId, ArtistName, Width, ArtLength, Encode, Date, IsPublic
                                ArtId = reader.GetInt32(0),
                                ArtName = reader.GetString(1),
                                ArtistId = reader.GetInt32(2),
                                ArtistName = reader[3] as string ?? string.Empty,
                                Width = reader.GetInt32(4),
                                ArtLength = reader.GetInt32(5),
                                Encode = reader.GetString(6),
                                CreationDate = reader.GetDateTime(7),
                                IsPublic = reader.GetBoolean(8)
                            };
                            paintings.Add(painting);
                        }
                    }
                }
            }
            return paintings;
        }

        public IEnumerable<GalleryArt> GetAllGalleryArt()
        {
            var paintings = new List<GalleryArt>();
            var connectionString = AppConfig.Value.ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //var query = "SELECT Date, TemperatureC, Summary FROM WeatherForecasts";
                var query = "SELECT ID, ArtName, Artistid, ArtistName, Width, ArtLength, Encode, CreationDate, isPublic FROM Art";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var painting = new GalleryArt
                            { //Art Table + NumLikes and NumComments
                                ArtId = reader.GetInt32(0),
                                ArtName = reader.GetString(1),
                                ArtistId = reader.GetInt32(2),
                                ArtistName = reader[3] as string ?? string.Empty,
                                Width = reader.GetInt32(4),
                                ArtLength = reader.GetInt32(5),
                                Encode = reader.GetString(6),
                                CreationDate = reader.GetDateTime(7),
                                IsPublic = reader.GetBoolean(8),
                                NumLikes = reader.GetInt32(9),
                                NumComments = reader.GetInt32(10)
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
                var query = "SELECT ID, ArtName, Artistid, ArtistName, Width, ArtLength, Encode, CreationDate, isPublic FROM Art WHERE ID=" + id;
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
                                ArtistName = reader[3] as string ?? string.Empty,
                                Width = reader.GetInt32(4),
                                ArtLength = reader.GetInt32(5),
                                Encode = reader.GetString(6),
                                CreationDate = reader.GetDateTime(7),
                                IsPublic = reader.GetBoolean(8)
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

