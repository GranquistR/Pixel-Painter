using Microsoft.Extensions.Options;
using MyTestVueApp.Server.Configuration;
using MyTestVueApp.Server.Entities;
using MyTestVueApp.Server.Interfaces;
using Microsoft.Data.SqlClient;

namespace MyTestVueApp.Server.ServiceImplementations
{
    public class LikeService : ILikeService
    {
        private IOptions<ApplicationConfiguration> AppConfig { get; }
        private ILogger<LikeService> Logger { get; }
        public LikeService(IOptions<ApplicationConfiguration> appConfig, ILogger<LikeService> logger)
        {
            AppConfig = appConfig;
            Logger = logger;
        }

        public async Task<int> InsertLike(int artId, string userId)
        {
            var connectionString = AppConfig.Value.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //Check to make sure the user hasnt already liked this work of art
                var checkDupQuery = "SELECT Count(*) FROM Likes WHERE ArtistID = @ArtistId AND ArtID = @ArtId";
                using (SqlCommand checkDupCommand = new SqlCommand(checkDupQuery, connection))
                {
                    checkDupCommand.Parameters.AddWithValue("@ArtistId", userId);
                    checkDupCommand.Parameters.AddWithValue("@ArtId", artId);

                    int count = (int) await checkDupCommand.ExecuteScalarAsync();
                    if (count > 0)
                    {
                        Console.WriteLine("This user has already liked this art piece!");
                        return 0;
                    }
                }

                var query = "INSERT INTO Likes (ArtistID, ArtID) VALUES (@ArtistId, @ArtId)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ArtistId", userId);
                    command.Parameters.AddWithValue("@ArtId", artId);

                    int rowsChanged = command.ExecuteNonQuery();
                    if (rowsChanged > 0)
                    {
                        Console.WriteLine("Like was inserted sucessfully!");
                        return rowsChanged;
                    } else {
                        Console.WriteLine("Failed to insert Like!");
                        return -1;
                    }
                }
            }
        }
        
        public async Task<int> RemoveLike(int artId, string userId)
        {
            var connectionString = AppConfig.Value.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //Check to make sure like exists
                 //Check to make sure the user hasnt already liked this work of art
                var checkDupQuery = "SELECT Count(*) FROM Likes WHERE ArtistID = @ArtistId AND ArtID = @ArtId";
                using (SqlCommand checkDupCommand = new SqlCommand(checkDupQuery, connection))
                {
                    checkDupCommand.Parameters.AddWithValue("@ArtistId", userId);
                    checkDupCommand.Parameters.AddWithValue("@ArtId", artId);

                    int count = (int) await checkDupCommand.ExecuteScalarAsync();
                    if (count == 0)
                    {
                        Console.WriteLine("The like you are trying to delete doesnt exist!");
                        return 0;
                    }
                }

                var query = "DELETE FROM Likes WHERE ArtistID = @ArtistId AND ArtID = @ArtId";
                using (SqlCommand command = new SqlCommand(query, connection)) 
                {
                    command.Parameters.AddWithValue("@ArtistId", userId);
                    command.Parameters.AddWithValue("@ArtId", artId);

                    int rowsChanged = command.ExecuteNonQuery();
                    if (rowsChanged > 0)
                    {
                        Console.WriteLine("Like was removed sucessfully!");
                        return rowsChanged;
                    } else {
                        Console.WriteLine("Failed to remove Like!");
                        return -1;
                    }
                }
            }
        }

        public async Task<bool> IsLiked(int artId, string userId) {
            var connectionString = AppConfig.Value.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                connection.Open();

                string likedQuery = "SELECT Count(*) FROM Likes WHERE ArtistID = @userId AND ArtID = @artId";
                using (SqlCommand command = new SqlCommand(likedQuery, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@artId", artId);

                    int count = (int) await command.ExecuteScalarAsync();

                    if (count > 0) {
                        Console.WriteLine("User has liked this artpiece!");
                        return true;
                    } else {
                        Console.WriteLine("User has not liked this artpiece!");
                        return false;
                    }
                }
            }
        }
    }
}