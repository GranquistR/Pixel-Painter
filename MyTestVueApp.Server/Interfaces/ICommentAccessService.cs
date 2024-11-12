using Microsoft.Data.SqlClient;
using MyTestVueApp.Server.Entities;

namespace MyTestVueApp.Server.Interfaces
{
    /// <summary>
    /// Interface defines the SQL service.
    /// </summary>
    public interface ICommentAccessService
    {
        Task<int> DeleteComment(int commentId);
        Task<int> EditComment(int commentId, string newMessage);

        /// <summary>
        /// Gets a list of all paintings from the database.
        /// </summary>
        /// <returns>A list of all paintings</returns>
        public IEnumerable<Comment> GetCommentsById(int id);
        public Task<Comment> CreateComment(Artist commenter, Comment comment);
    }
}