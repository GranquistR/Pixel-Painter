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

        public async Task<int> InsertLike(int artId, Artist artist)
        {
            var connectionString = AppConfig.Value.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //Check to make sure the user hasnt already liked this work of art
                var checkDupQuery = "SELECT Count(*) FROM Likes WHERE ArtistID = @ArtistId AND ArtID = @ArtId";
                using (SqlCommand checkDupCommand = new SqlCommand(checkDupQuery, connection))
                {
                    checkDupCommand.Parameters.AddWithValue("@ArtistId", artist.id);
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
                    command.Parameters.AddWithValue("@ArtistId", artist.id);
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
        
        public async Task<int> RemoveLike(int artId, Artist artist)
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
                    checkDupCommand.Parameters.AddWithValue("@ArtistId", artist.id);
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
                    command.Parameters.AddWithValue("@ArtistId", artist.id);
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

        public async Task<bool> IsLiked(int artId, Artist artist) {
            var connectionString = AppConfig.Value.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                connection.Open();

                string likedQuery = "SELECT Count(*) FROM Likes WHERE ArtistId = @ArtistId AND ArtID = @ArtID";
                using (SqlCommand command = new SqlCommand(likedQuery, connection))
                {
                    command.Parameters.AddWithValue("@ArtistId", artist.id);
                    command.Parameters.AddWithValue("@ArtID", artId);

                    int count = (int) await command.ExecuteScalarAsync();

                    if (count > 0) {
                        return true;
                    } else {
                        return false;
                    }
                }
            }
        }
    }
}