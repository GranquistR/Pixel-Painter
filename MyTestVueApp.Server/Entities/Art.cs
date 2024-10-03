namespace MyTestVueApp.Server.Entities
{ // ArtId, ArtName, ArtistId, Width, ArtLength, Encode, CreationDate, IsPublic
    public class Art
    {
        public int ArtId {get; set;}

        public string? ArtName { get; set; }

        public int ArtistId { get; set; }

        public int Width { get; set;}

        public int ArtLength { get; set;}

        public string? Encode { get; set;}

        public DateTime CreationDate { get; set; }

        public bool IsPublic { get; set; }

        /*
        public Art(int ArtId, string ArtName, int ArtistId, int Width, int ArtLength, string Encode, DateTime Date, bool IsPublic) {
            this.ArtId = ArtId;
            this.ArtName = ArtName;
            this.ArtistId = ArtistId;
            this.Width = Width;
            this.ArtLength = ArtLength;
            this.Encode = Encode;
            this.Date = Date;
            this.IsPublic = IsPublic;
        }*/
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