namespace MyTestVueApp.Server.Entities
{
    public class ArtPainting
    {
        public int ArtId { get; set; }
        public string ArtName { get; set; }

        public int ArtistId { get; set; }

        public int ArtWidth { get; set; }

        public int ArtHeight { get; set; }

        public string Encode {  get; set; }

        public int CreationDate { get; set; }

        public bool isPublic { get; set; }

        public int PK_Art { get; set; }

        public int FK_Art { get; set; }
    }

    public class Artist 
    {
        public int ArtId { get; set; }
        public string ArtistName { get; set; }

        public string token { get; set; }

        public bool isAdmin { get; set; }

        public int CreationDate { get; set; }


    }

}
