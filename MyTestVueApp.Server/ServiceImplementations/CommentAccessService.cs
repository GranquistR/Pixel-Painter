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
        public async Task<int> EditComment(int commentId, string newMessage)
        {
            var connectionString = AppConfig.Value.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //Check to make sure the user hasnt already liked this work of art
                var checkDupQuery = "SELECT Count(*) FROM comment WHERE ID = @CommentId";
                using (SqlCommand checkDupCommand = new SqlCommand(checkDupQuery, connection))
                {
                    checkDupCommand.Parameters.AddWithValue("@CommentId", commentId);

                    int count = (int)await checkDupCommand.ExecuteScalarAsync();
                    if (count == 0)
                    {
                        Console.WriteLine("No comment exists to edit!");
                        return 0;
                    }
                    if (count > 1)
                    {
                        Console.WriteLine("more than one comment exists to edit!");
                        return 0;
                    }
                }
                //update table here
                var query = "UPDATE Comment SET Comment = @newComment WHERE ID = @CommentId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@newComment", newMessage);
                    command.Parameters.AddWithValue("@CommentId", commentId);

                    int rowsChanged = command.ExecuteNonQuery();
                    if (rowsChanged > 0)
                    {
                        Console.WriteLine("Comment was changed sucessfully!");
                        return rowsChanged;
                    }
                    else
                    {
                        Console.WriteLine("Failed to edit comment!");
                        return -1;
                    }
                }
            }
        }

        public async Task<int> DeleteComment(int commentId)
        {
            var connectionString = AppConfig.Value.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //Check to make sure the there is a comment to delete
                var checknullQuery = "SELECT Count(*) FROM comment WHERE ID = @CommentId";
                using (SqlCommand checkDupCommand = new SqlCommand(checknullQuery, connection))
                {
                    checkDupCommand.Parameters.AddWithValue("@CommentId", commentId);

                    int count = (int)await checkDupCommand.ExecuteScalarAsync();
                    if (count == 0)
                    {
                        Console.WriteLine("No comment exists to delete!");
                        return 0;
                    }
                    if (count > 1)
                    {
                        Console.WriteLine("more than one comment with same ID!");
                        return 0;
                    }
                }
                var checkResponseQuery = "SELECT Count(*) FROM comment WHERE Response = @CommentId";
                using (SqlCommand checkResponseCommand = new SqlCommand(checknullQuery, connection))
                {
                    checkResponseCommand.Parameters.AddWithValue("@CommentId", commentId);

                    int count = (int)await checkResponseCommand.ExecuteScalarAsync();
                    if (count > 0)
                    {
                        var subquery = "DELETE Comment WHERE Response = @commentId";
                        using (SqlCommand DeleteResponse = new SqlCommand(subquery, connection))
                        {
                            DeleteResponse.Parameters.AddWithValue("@CommentId", commentId);

                            int rowsChanged = DeleteResponse.ExecuteNonQuery();
                            if (rowsChanged > 0)
                            {
                                Console.WriteLine("Response Comments Deleted");
                            }
                        }
                    }
                }
                //update table here
                var query = "Delete Comment WHERE ID = @CommentId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CommentId", commentId);

                    int rowsChanged = command.ExecuteNonQuery();
                    if (rowsChanged > 0)
                    {
                        Console.WriteLine("Comment was Deleted sucessfully!");
                        return rowsChanged;
                    }
                    else
                    {
                        Console.WriteLine("Failed to edit comment!");
                        return -1;
                    }
                }
            }

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

                    string ArtistName = (string)await duplicateCommand.ExecuteScalarAsync();
                    //if (count > 0)
                    //{
                    //    Console.WriteLine("Placeholder");
                    //    return false;
                    //}

                    var insertQuery = "INSERT INTO Comment (ArtID,ArtistId,ArtistName,Comment) VALUES (@ArtId,@ArtistID,@ArtistName,@Comment)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@ArtID", ArtId);
                        insertCommand.Parameters.AddWithValue("@ArtistID", userID);
                        insertCommand.Parameters.AddWithValue("@ArtistName", ArtistName);
                        insertCommand.Parameters.AddWithValue("@Comment", comment);
                        //int s = 1;
                        try
                        {
                            int rowschanged = (int)await insertCommand.ExecuteNonQueryAsync();


                            if (rowschanged > 0)
                            {
                                Console.WriteLine("Comment has been successfully added!");
                                return true;
                            }
                            else
                            {
                                Console.WriteLine("Comment has not been made");
                                return false;
                            }
                        }
                        catch (Exception ex) { Console.WriteLine(ex.ToString()); return false; }
                    }
                }

            }
        }
    }
}
