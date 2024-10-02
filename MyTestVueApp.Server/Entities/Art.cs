namespace MyTestVueApp.Server.Entities
{ // ArtId, ArtName, ArtistId, Width, ArtLength, Encode, Date, IsPublic
    public class Art
    {
        public int ArtId {get; set;}

        public string? ArtName { get; set; }

        public int ArtistId { get; set; }

        public int Width { get; set;}

        public int ArtLength { get; set;}

        public string? Encode { get; set;}

        public DateTime Date { get; set; }

        public int IsPublic { get; set; }
    }
}

/* Based on this table:
ID int IDENTITY(1,1) NOT NULL,
    ArtName varchar(255),
	Artistid int,
    Width int,
    ArtLength int,
    Encode varchar(max),
    CreationDate DATETIME DEFAULT GETDATE(),
    isPublic bit DEFAULT 0,
*/