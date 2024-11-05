using Microsoft.Extensions.Options;
using MyTestVueApp.Server.Configuration;
using MyTestVueApp.Server.Entities;
using MyTestVueApp.Server.Interfaces;
using Microsoft.Data.SqlClient;

namespace MyTestVueApp.Server.ServiceImplementations
{
    public class CommentAccessService : ICommentAccessService
    {
        private IOptions<ApplicationConfiguration> AppConfig { get; }
        private ILogger<CommentAccessService> Logger { get; }
        public CommentAccessService(IOptions<ApplicationConfiguration> appConfig, ILogger<CommentAccessService> logger)
        {
            AppConfig = appConfig;
            Logger = logger;
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