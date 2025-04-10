using MyTestVueApp.Server.Interfaces;
using MyTestVueApp.Server.Entities;

namespace MyTestVueApp.Server.ServiceImplementations
{
    public class ConnectionManager : IConnectionManager
    {
        // groupName, Group
        Dictionary<string, Group> groups = new();
        Dictionary<string, Artist> artistLookup = new();
        List<MembershipRecord> records = new();

        public void AddGroup(string groupName, string[][] canvas, int canvasSize, string backgroundColor)
        {
            groups.Add(groupName, new Group(groupName, canvas, canvasSize, backgroundColor));
        }

        public void AddUser(string connectionId, Artist artist, string groupName)
        {
            Console.WriteLine("AddUser: (" + connectionId + ", " + artist.id + ", " + groupName);
            if (groups.ContainsKey(groupName))
            {
                artistLookup.Add(connectionId, artist);
                groups[groupName].AddMember(artist);
                if (records.Any(mr => mr.ArtistId == artist.id))
                {
                    Console.WriteLine("User already exists, so adding to connections!");
                    records.Find(mr => mr.ArtistId == artist.id).Connections.Add(new(connectionId, groupName));
                } else
                {
                    Console.WriteLine("New User!");
                    records.Add(new(connectionId, artist.id, groupName));
                }
            }

        }

        public void RemoveUserFromGroup(string connectionId, Artist artist, string groupName)
        {
            Console.WriteLine("#Users before removal: " + groups[groupName].CurrentMembers.Count);

            MembershipRecord? record = records.Find(rec => rec.ArtistId == artist.id);
            if (record == null)
            {
                Console.WriteLine("User Doesnt Exist so cant be deleted!");
                return;
            }

            List<ConnectionBinding> allUserConnections = new();
            ConnectionBinding? connectionToDelete = null;
            foreach(ConnectionBinding binding in record.Connections)
            {

                if (binding.groupName == groupName)
                {
                    allUserConnections.Add(binding);
                    if (binding.connectionId == connectionId)
                    {
                        connectionToDelete = binding;
                    }
                }    
            }

            if (connectionToDelete == null || allUserConnections.Count == 0) 
            { // Invalid request
                Console.WriteLine("RemoveUserFromGroup: Invalid ConnectionId!");
                return;
            }
            
            if (allUserConnections.Count() == 1)
            { // Remove member from group
                groups[groupName].RemoveMember(artist);
                record.Connections.Remove(connectionToDelete);
                artistLookup.Remove(connectionId);
            }else
            { // Just remove the connection
                record.Connections.Remove(connectionToDelete);
                artistLookup.Remove(connectionId);
            }

            if (record.Connections.Count == 0)
            { // Remove record from records if the Artist doesnt have any open connections;
                Console.WriteLine("Artist has zero connections. Removing from List!");
                records.Remove(record);
            }
          
            Console.WriteLine("Remaining Users: " +  groups[groupName].CurrentMembers.Count);
            if (groups[groupName].IsEmpty())
            { // Delete group if it is empty
                Console.WriteLine("Deleting Group");
                groups.Remove(groupName);
            }
        }

        public void RemoveUserFromAllGroups(string connectionId)
        {
            Artist artist = artistLookup[connectionId];
            MembershipRecord? record = records.Find(rec => rec.ArtistId == artist.id);
            if (record == null)
            {
                return;

            }
            HashSet<string> groups = new();
            foreach (ConnectionBinding cb in record.Connections) {
                groups.Add(cb.groupName);
            }

            foreach (string groupName in groups)
            {
                RemoveUserFromGroup(connectionId, artist, groupName);
            }
            
        }

        public IEnumerable<Group> GetGroups()
        {
            return groups.Values;
        }

        public IEnumerable<GroupAdvert> GetGroupAdverts()
        {
            List<GroupAdvert> groupAdverts = new();
            foreach (Group group in  groups.Values)
            {
                groupAdverts.Add(new GroupAdvert(group.Name, group.CurrentMembers.Count));
            }
            return groupAdverts;
        }

        public IEnumerable<Artist> GetUsersInGroup(string groupName)
        {
            return groups[groupName].CurrentMembers;
        }

        public IEnumerable<Artist> GetContributingArtists(string groupName)
        {
            return groups[groupName].MemberRecord;
        }

        public void PaintPixels(string groupName, string color, Coordinate[] vector)
        {
            groups[groupName].PaintPixels(color, vector);
        }

        public void RemoveGroup(string groupName)
        {
            groups.Remove(groupName);
        }

        public Group GetGroup(string groupName)
        {
            return groups[groupName];
        }

        public bool GroupExists(string groupName)
        {
            Console.WriteLine("GroupExists!");
            return groups.ContainsKey(groupName);
        }
    }
}
