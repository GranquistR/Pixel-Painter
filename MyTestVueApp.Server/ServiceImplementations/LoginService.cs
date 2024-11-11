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
using MyTestVueApp.Server.Entities;
using MyTestVueApp.Server.Interfaces;
using System;
using System.Security.Authentication;

namespace MyTestVueApp.Server.ServiceImplementations
{
    public class LoginService : ILoginService
    {
        private IOptions<ApplicationConfiguration> AppConfig { get; }
        private ILogger<LoginService> Logger { get; }

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

        public async Task SendIdToDatabase(string subId)
        {
            var artist = await GetUserBySubId(subId);
            if (artist == null)
            {

                var connectionString = AppConfig.Value.ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var query = "INSERT INTO Artist (Name, SubId, IsAdmin, CreationDate) VALUES (@Name, @SubId, @IsAdmin, @CreationDate)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", "Username");
                        command.Parameters.AddWithValue("@SubId", subId);
                        command.Parameters.AddWithValue("@IsAdmin", 0);
                        command.Parameters.AddWithValue("@CreationDate", DateTime.UtcNow);

                        command.ExecuteNonQuery();

                    }
                }
            }
        }

        public async Task<Artist> GetUserBySubId(string subId)
        {
            var artist = new Artist();

            var connectionString = AppConfig.Value.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var query = @"
                    SELECT TOP (1) [Id]
                          ,[SubId]
                          ,[Name]
                          ,[IsAdmin]
                          ,[CreationDate]
                      FROM [PixelPainter].[dbo].[Artist]
                      WHERE SubId = @SubId
                    ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SubId", subId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            artist = new Artist
                            {
                                Id = reader.GetInt32(0),
                                SubId = reader.GetString(1),
                                Name = reader.GetString(2),
                                IsAdmin = reader.GetBoolean(3),
                                CreationDate = reader.GetDateTime(4),
                            };
                            return artist;
                        }
                    }
                }
            }

            return null;
        }
    }
}
