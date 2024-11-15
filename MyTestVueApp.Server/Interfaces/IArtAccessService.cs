using Microsoft.Data.SqlClient;
using MyTestVueApp.Server.Entities;

namespace MyTestVueApp.Server.Interfaces
{
    /// <summary>
    /// Interface defines the SQL service.
    /// </summary>
    public interface IArtAccessService
    {

        /// <summary>
        /// Gets a list of all paintings from the database.
        /// </summary>
        /// <returns>A list of all paintings</returns>
        public IEnumerable<Art> GetAllArt();
        public Art GetArtById(int id);
        public Task<int> DeleteArt(int artId);
        public Task<Art> SaveNewArt(Artist artist, Art art);
        public Task<Art> UpdateArt(Artist artist, Art art);
    }
}