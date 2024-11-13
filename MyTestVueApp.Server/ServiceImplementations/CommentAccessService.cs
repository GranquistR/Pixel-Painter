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
            try
            {
                var comments = new List<Comment>();
                var connectionString = AppConfig.Value.ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //var query = "SELECT Date, TemperatureC, Summary FROM WeatherForecasts";
                    var query = @$"
                   SELECT 
                        Comment.Id, 
                        Comment.ArtistId, 
                        Comment.ArtID, 
                        Comment.[Message],
	                    Artist.[Name] as CommenterName,
                        Comment.CreationDate,
                        Comment.ReplyId
                    FROM Comment  
                    JOIN Artist ON Artist.id = Comment.ArtistId
                    WHERE ArtID = @id
                    Order By CreationDate DESC;";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@id", id));
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var comment = new Comment
                                { //Art Table + NumLikes and NumComments
                                    id = reader.GetInt32(0),
                                    artistId = reader.GetInt32(1),
                                    artId = reader.GetInt32(2),
                                    message = reader.GetString(3),
                                    commenterName = reader.GetString(4),
                                    creationDate = reader.GetDateTime(5),
                                    replyId = reader.IsDBNull(6) ? -1 : reader.GetInt32(6)
                                };
                                comments.Add(comment);
                            }
                        }
                    }
                }
                return comments;
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, "Error retrieving comments");
                throw;
            }
        }
        public async Task<Comment> CreateComment(Artist commenter, Comment comment)
        {
            comment.artistId = commenter.Id;
            comment.commenterName = commenter.Name;
            comment.creationDate = DateTime.UtcNow;
           
            using (SqlConnection connection = new SqlConnection(AppConfig.Value.ConnectionString))
            {
                try
                {
                    connection.Open();
                    var insertQuery = "INSERT INTO Comment (ArtistId,ArtId,Message,CreationDate) VALUES (@ArtistID,@ArtID,@Message,@CreationDate)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ArtistID", commenter.Id);
                        command.Parameters.AddWithValue("@ArtID", comment.artId);
                        command.Parameters.AddWithValue("@Message", comment.message);
                        command.Parameters.AddWithValue("@CreationDate", DateTime.UtcNow);

                        var newId = await command.ExecuteScalarAsync();
                        comment.id = Convert.ToInt32(newId);

                        return comment;

                    }
                }
                catch (Exception ex)
                {
                    Logger.LogCritical(ex, "Failed to insert comment");
                    throw;
                }
            }

        }
    }
}

