using MyTestVueApp.Server.Interfaces;
using MyTestVueApp.Server.Entities;
using System.Numerics;

namespace MyTestVueApp.Server.ServiceImplementations
{
    public class ConnectionManager : IConnectionManager
    {
        Dictionary<string, Group> groups = new();
        public void AddGroup(string groupName)
        {
            int canvasSize = 64; // TEMP
            groups.Add(groupName, new Group(groupName));
        }

        public void AddUser(string groupName, string userName)
        { // Add a user to group 
            if (groups.ContainsKey(groupName))
            {
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

        public bool IsGroupEmpty(string groupName)
        {
            if (!groups.ContainsKey(groupName))
            {
                return true;
            }
            return false;
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
    }
}
