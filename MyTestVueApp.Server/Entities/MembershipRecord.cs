namespace MyTestVueApp.Server.Entities
{
    public class MembershipRecord
    {
        public Artist Artist { get; set; }
        public string GroupName { get; set; }

        public MembershipRecord(Artist artist, string group)
        {
            Artist = artist;
            GroupName = group;
        } 
    }
}
