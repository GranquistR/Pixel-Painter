namespace MyTestVueApp.Server.Entities
{
    public class Art
    {
        //Required
        public int id { get; set; }
        public string artistId { get; set; }
        public string title { get; set; }
        public bool isPublic { get; set; }
        public DateTime creationDate { get; set; }

        public string? ArtName { get; set; }

        //Optional external values
        public string artistName { get; set; }
        public int numLikes { get; set; }
        public int numComments { get; set; }

    }

}
