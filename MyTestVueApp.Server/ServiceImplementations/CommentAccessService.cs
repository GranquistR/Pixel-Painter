using Microsoft.Extensions.Options;
using MyTestVueApp.Server.Configuration;
using MyTestVueApp.Server.Entities;
using MyTestVueApp.Server.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Oauth2.v2;
using Google.Apis.Services;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Components.Web.Virtualization;


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
        public async Task<bool> createComment(string userID, string comment, int ArtId)
        {
            var connectionString = AppConfig.Value.ConnectionString;
            Logger.LogInformation("One hit");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var duplicateQuery = "SELECT artist.ArtistName FROM Artist WHERE Artist.ID=@ArtistId";
                using (SqlCommand duplicateCommand = new SqlCommand(duplicateQuery, connection))
                {
                    duplicateCommand.Parameters.AddWithValue("@ArtistId", userID);

                    string ArtistName =  (String)await duplicateCommand.ExecuteScalarAsync();
                    //if (count > 0)
                    //{
                    //    Console.WriteLine("Placeholder");
                    //    return false;
                    //}

                    var insertQuery = "INESRT INTO Comment (ArtID,ArtistID,ArtistName,Comment) VALUES (@ArtId,@ArtistId,@ArtistName,@Comment)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@ArtID", ArtId);
                        insertCommand.Parameters.AddWithValue("@ArtisttID", userID);
                        insertCommand.Parameters.AddWithValue("@ArtistName", ArtistName);
                        insertCommand.Parameters.AddWithValue("@Comment", comment);

                        int rowsChanged = (int)await insertCommand.ExecuteScalarAsync();
                        if (rowsChanged > 0)
                        {
                            Console.WriteLine("Comment has been successfully added!");
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

            }
        }
    }
}
