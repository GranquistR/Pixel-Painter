using MyTestVueApp.Server.Interfaces;
using MyTestVueApp.Server.Entities;

namespace MyTestVueApp.Server.ServiceImplementations
{
    public class ConnectionManager : IConnectionManager
    {
        Dictionary<string, Group> groups = new();
        public void AddGroup(string groupName)
        {
            groups.Add(groupName, new Group(groupName));
        }

        public void AddGroup(string groupName, string[][] canvas, int canvasSize, string backgroundColor)
        {
            groups.Add(groupName, new Group(groupName, canvas, canvasSize, backgroundColor));
        }

        public void AddUser(string groupName, string userName)
        { // Add a user to group 
            if (groups.ContainsKey(groupName))
            { // Just add User
                groups[groupName].AddMember(userName);

            } else
            {
                groups.Add(groupName, new Group(groupName));
                groups[groupName].AddMember(userName);
            }
        }

        public void RemoveUser(string groupName, string userName)
        {
            groups[groupName].RemoveMember(userName);
            if (groups[groupName].IsEmpty())
            {
                groups.Remove(groupName);
            }
        }

        public IEnumerable<string> GetGroups()
        {
            return groups.Keys;
        }

        public IEnumerable<string> GetUsersInGroup(string groupName)
        {
            return groups[groupName].Members;
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
