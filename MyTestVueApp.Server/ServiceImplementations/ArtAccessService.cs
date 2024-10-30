using Microsoft.Extensions.Options;
using MyTestVueApp.Server.Configuration;
using MyTestVueApp.Server.Entities;
using MyTestVueApp.Server.Interfaces;
using Microsoft.Data.SqlClient;

namespace MyTestVueApp.Server.ServiceImplementations
{
    public class ArtAccessService : IArtAccessService
    {
        private IOptions<ApplicationConfiguration> AppConfig { get; }
        private ILogger<ArtAccessService> Logger { get; }
        public ArtAccessService(IOptions<ApplicationConfiguration> appConfig, ILogger<ArtAccessService> logger)
        {
            AppConfig = appConfig;
            Logger = logger;
        }

        public IEnumerable<Art> GetAllArt()
        {
            var paintings = new List<Art>();
            var connectionString = AppConfig.Value.ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //var query = "SELECT Date, TemperatureC, Summary FROM WeatherForecasts";
                var query =
                    "Select Art.ID, Art.ArtName, Art.Artistid, Art.ArtistName, Art.Width, Art.ArtLength, Art.Encode, Art.CreationDate, Art.isPublic,COUNT(distinct Likes.ID) as Likes, Count(distinct Comment.ID) as Comments " +
                    "FROM ART " +
                    "LEFT JOIN Likes ON Art.ID = Likes.ArtID " +
                    "LEFT JOIN Comment ON Art.ID = Comment.ArtID " +
                    "GROUP BY Art.ID, Art.ArtName, Art.Artistid, Art.ArtistName, Art.Width, Art.ArtLength, Art.Encode, Art.CreationDate, Art.isPublic";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var painting = new Art
                            { //Art Table + NumLikes and NumComments
                                ArtId = reader.GetInt32(0),
                                ArtName = reader.GetString(1),
                                ArtistId = reader.GetString(2),
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
            var painting = new Art();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //var query = "SELECT Date, TemperatureC, Summary FROM WeatherForecasts";
                var query = 
                    "Select Art.ID, Art.ArtName, Art.Artistid, Art.ArtistName, Art.Width, Art.ArtLength, Art.Encode, Art.CreationDate, Art.isPublic,COUNT(distinct Likes.ID) as Likes, Count(distinct Comment.ID) as Comments " +
                    "FROM ART " +
                    "LEFT JOIN Likes ON Art.ID = Likes.ArtID " +
                    "LEFT JOIN Comment ON Art.ID = Comment.ArtID " +
                    "WHERE Art.ID=" + id + " " +
                    "GROUP BY Art.ID, Art.ArtName, Art.Artistid, Art.ArtistName, Art.Width, Art.ArtLength, Art.Encode, Art.CreationDate, Art.isPublic ";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            painting = new Art
                            { //ArtId, ArtName, ArtistId, Width, ArtLength, Encode, Date, IsPublic
                                ArtId = reader.GetInt32(0),
                                ArtName = reader.GetString(1),
                                ArtistId = reader.GetString(2),
                                ArtistName = reader[3] as string ?? string.Empty,
                                Width = reader.GetInt32(4),
                                ArtLength = reader.GetInt32(5),
                                Encode = reader.GetString(6),
                                CreationDate = reader.GetDateTime(7),
                                IsPublic = reader.GetBoolean(8)
                            };
                            
                        }
                    }
                }
            }
            return painting;
        }

        public IEnumerable<Comment> GetCommentsById(int id)
        {
            var comments = new List<Comment>();
            var connectionString = AppConfig.Value.ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //var query = "SELECT Date, TemperatureC, Summary FROM WeatherForecasts";
                var query = 
                    "SELECT ID, ArtistID, ArtistName, ArtID, Comment, CommentTime FROM Comment " +
                    "WHERE ArtID=" + id + "AND Response IS NULL " + 
                    "Order By CommentTime";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var comment = new Comment
                            { //Art Table + NumLikes and NumComments
                                CommentId = reader.GetInt32(0),
                                ArtistId = reader.GetString(1),
                                ArtistName = reader.GetString(2),
                                ArtId = reader.GetInt32(3),
                                CommentContent = reader.GetString(4),
                                CommentTime = reader.GetDateTime(5)
                            };
                            comments.Add(comment);
                        }
                    }
                }
            }
            return comments;
        }
    }
}

