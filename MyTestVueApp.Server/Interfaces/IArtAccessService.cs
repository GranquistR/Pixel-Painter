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
        public Task<Art> SaveArt(Artist artist, Art art);
        public IEnumerable<Comment> GetCommentsById(int id);
    }
}