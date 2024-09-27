using Microsoft.Extensions.Options;
using MyTestVueApp.Server.Configuration;
using MyTestVueApp.Server.Entities;
using MyTestVueApp.Server.Interfaces;
using Microsoft.Data.SqlClient;

namespace MyTestVueApp.Server.ServiceImplementations
{
    public class ArtService : IArtServices
    {
        private IOptions<ApplicationConfiguration> AppConfig { get; }
        public ArtService(IOptions<ApplicationConfiguration> appConfig)
        {
            AppConfig = appConfig;
        }

        public IEnumerable<ArtPainting> GetArtService()
        {
            var paintings = new List<ArtPainting>();
            var connectionString = AppConfig.Value.ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT FROM ";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var painting = new ArtPainting
                            {

                            };
                            paintings.Add(painting);
                        }
                    }
                }
            }

            return paintings;
        }
    }
}
