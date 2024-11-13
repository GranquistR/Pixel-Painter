using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;
using Google.Apis.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using MyTestVueApp.Server.Configuration;
using MyTestVueApp.Server.Interfaces;
using System;
using System.Data;

namespace MyTestVueApp.Server.ServiceImplementations
{
    public class LoginService : ILoginService
    {
        private IOptions<ApplicationConfiguration> AppConfig { get; }
        private ILogger<LoginService> Logger { get; }

        private string[] adjectives = new string[]
        {
            "Happy", "Sad", "Bright", "Dark", "Quick",
            "Slow", "Loud", "Soft", "Smooth", "Rough",
            "Imploding", "Cold", "New", "Old", "Rich",
            "Poor", "Clean", "Dirty", "Easy", "Hard",
            "Funny", "Serious", "Friendly", "Angry", "Calm",
            "Bitter", "Sweet", "Sour", "Spicy", "Salty",
            "Beautiful", "Ugly", "Strong", "Weak", "Heavy",
            "Light", "Tall", "Short", "Wide", "Narrow",
            "Colorful", "Dull", "Busy", "Quiet", "Famous",
            "Unknown", "Happy", "Lazy", "Active", "Thoughtful"
        };

        private string[] nouns = new string[]
        {
            "Apple", "Ball", "Cat", "Dog", "Elephant",
            "Flower", "Guitar", "House", "Ice", "Jacket",
            "Kite", "Lion", "Mountain", "Notebook", "Orange",
            "Pencil", "Quilt", "Rocket", "Sun", "Tree",
            "Umbrella", "Violin", "Window", "Car", "Yacht",
            "Zebra", "Book", "Car", "Desk", "Egg",
            "Fish", "Glove", "Hat", "Island", "Jam",
            "Key", "Lamp", "Mug", "Nut", "Oven",
            "Pizza", "Quokka", "Ring", "Shoes", "Turtle",
            "Vase", "Whistle", "Dancer", "Yarn", "Zipper"
        };

        public LoginService(IOptions<ApplicationConfiguration> appConfig, ILogger<LoginService> logger)
        {
            AppConfig = appConfig;
            Logger = logger;
        }

        public async Task<string> GetUserId(string code)
        {
            var accessToken = await GetAccessToken(code);

            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken);
                var oauth2Service = new Oauth2Service(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "MyTestVueApp"
                });

                Userinfo userInfo = await oauth2Service.Userinfo.Get().ExecuteAsync();
                return userInfo.Id;
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, "Error in GetUserEmail");
                throw;
            }
        }

        private async Task<string> GetAccessToken(string code)
        {
            try
            {
                var tokenResponse = new AuthorizationCodeTokenRequest
                {
                    ClientId = AppConfig.Value.ClientId,
                    ClientSecret = AppConfig.Value.ClientSecret,
                    Code = code,
                    GrantType = "authorization_code",
                    RedirectUri = AppConfig.Value.HomeUrl + "login/LoginRedirect"
                };

                var result = await tokenResponse.ExecuteAsync(new HttpClient(), GoogleAuthConsts.TokenUrl, CancellationToken.None, SystemClock.Default);
                return result.AccessToken;
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, "Error in GetSubId");
                throw;
            }
        }

        public async Task<int> SendIdToDatabase(string subId)
        {
            var connectionString = AppConfig.Value.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // This query and command is to check if the user and Id is already in the database
                var checkDupQuery = "SELECT COUNT(*) FROM Artist WHERE ID = @ID";
                using (SqlCommand checkDupCommand = new SqlCommand(checkDupQuery, connection))
                {
                    checkDupCommand.Parameters.AddWithValue("@ID", subId);

                    int count = (int)await checkDupCommand.ExecuteScalarAsync();
                    if (count > 0)
                    {
                        return 0;
                    }
                }

                var query = "INSERT INTO Artist (ID, ArtistName) VALUES (@ID, @ArtistName)";
                string username = generateUsername().Result;

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", subId);
                    command.Parameters.AddWithValue("@ArtistName", username);

                    int rowsChanged = command.ExecuteNonQuery();
                    if (rowsChanged > 0)
                    {
                        return rowsChanged;
                    } 
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        public string getAdjective(int index)
        {
            return adjectives[index];
        }

        public string getNoun(int index)
        {
            return nouns[index];
        }

        public async Task<string> generateUsername()
        {
            Random rnd = new Random();

            var username = getAdjective(rnd.Next(50)) + getNoun(rnd.Next(50)) + rnd.Next(1000);

            return username;
        }

        public async Task<string> getUsername(string subId)
        {
            string query = "SELECT ArtistName FROM Artist WHERE ID = @ID";

            var connectionString = AppConfig.Value.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", subId);

                    using (SqlDataReader read = command.ExecuteReader())
                    {
                        if (read.Read()) {
                            string username = read["ArtistName"].ToString();
                            return username;
                        }
                    }
                }
            }
            return "Failed to get username";
        }

        public async Task<int> updateUsername(string newUsername, string subId)
        {
            var connectionString = AppConfig.Value.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // This query and command is to check if the username is already taken or not
                var checkDupQuery = "SELECT COUNT(*) FROM Artist WHERE ArtistName = @ArtistName";
                using (SqlCommand checkDupCommand = new SqlCommand(checkDupQuery, connection))
                {
                    checkDupCommand.Parameters.AddWithValue("@ArtistName", newUsername);

                    int count = (int)await checkDupCommand.ExecuteScalarAsync();
                    if (count > 0)
                    {
                        return 0;
                    }
                }

                // var query = "INSERT INTO Artist (ArtistName) VALUES (@ArtistName)";
                var query = "UPDATE Artist SET ArtistName = @ArtistName WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ID", SqlDbType.VarChar).Value = subId;
                    command.Parameters.AddWithValue("@ArtistName", newUsername);

                    int rowsChanged = command.ExecuteNonQuery();
                    if (rowsChanged > 0)
                    {
                        return rowsChanged;
                    } 
                    else
                    {
                        return -1;
                    }
                }
            }
        }
    }
}
