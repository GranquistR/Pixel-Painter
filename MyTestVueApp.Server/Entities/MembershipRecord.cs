namespace MyTestVueApp.Server.Entities
{
    public class MembershipRecord(Artist artist, string group)
    {
        public Artist Artist { get; set; } = artist;
        public string GroupName { get; set; } = group;
    }
}
