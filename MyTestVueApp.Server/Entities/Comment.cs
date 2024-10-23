namespace MyTestVueApp.Server.Entities
{ // ArtId, ArtName, ArtistId, Width, ArtLength, Encode, CreationDate, IsPublic
    public class Comment
    { // Art Table
        public int CommentId { get; set; }

        public int ArtistId { get; set; }

        public string? ArtistName { get; set; }

        public int ArtId { get; set; }

        public string? CommentContent { get; set; }

        public int Response {get; set; }

        public DateTime CommentTime { get; set; }

    }
}