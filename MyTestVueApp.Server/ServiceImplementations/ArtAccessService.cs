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
                            var pixelGrid = new PixelGrid()
                            {
                                width = reader.GetInt32(4),
                                height = reader.GetInt32(5),
                                encodedGrid = reader.GetString(6)
                            };
                            var painting = new Art
                            { //Art Table + NumLikes and NumComments
                                id = reader.GetInt32(0),
                                title = reader.GetString(1),
                                artistId = reader.GetString(2),
                                artistName = reader[3] as string ?? string.Empty,
                                creationDate = reader.GetDateTime(7),
                                isPublic = reader.GetBoolean(8),
                                numLikes = reader.GetInt32(9),
                                numComments = reader.GetInt32(10),
                                pixelGrid = pixelGrid,
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

                            var pixelGrid = new PixelGrid()
                            {
                                width = reader.GetInt32(4),
                                height = reader.GetInt32(5),
                                encodedGrid = reader.GetString(6)
                            };
                            painting = new Art
                            { //ArtId, ArtName, ArtistId, Width, ArtLength, Encode, Date, IsPublic
                                id = reader.GetInt32(0),
                                title = reader.GetString(1),
                                artistId = reader.GetString(2),
                                artistName = reader[3] as string ?? string.Empty,
                                pixelGrid = pixelGrid,
                                creationDate = reader.GetDateTime(7),
                            };

                        }
                    }
                }
            }
            return painting;
        }

        public Art SaveArt(Art art)
        {

            





            return art;
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
                                ArtistId = reader.GetInt32(1),
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

