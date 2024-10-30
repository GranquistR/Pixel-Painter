namespace MyTestVueApp.Server.Entities
{ // ArtId, ArtName, ArtistId, Width, ArtLength, Encode, CreationDate, IsPublic
    public class Art
    { // Art Table
        public int ArtId { get; set; }

        public string? ArtName { get; set; }

        public string ArtistId { get; set; }

        public string? ArtistName { get; set; }

        public int Width { get; set; }

        public int ArtLength { get; set; }

        public string? Encode { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsPublic { get; set; }

        // Likes
        public int NumLikes { get; set; }

        //Comments
        public int NumComments { get; set; }

    }
}