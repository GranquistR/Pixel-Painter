using MyTestVueApp.Server.Entities;
using System.Numerics;

namespace MyTestVueApp.Server.Interfaces
{
    public interface IConnectionManager
    {
        public void AddGroup(string groupName);
        public void AddGroup(string groupName, string[][] canvas, int canvasSize, string backgroundColor);
        public void RemoveGroup(string groupName);
        public void AddUser(string groupName, string userName);
        public void RemoveUser(string groupName, string userName);
        public void PaintPixels(string groupName, string color, Coordinate[] vector);
        public Group GetGroup(string groupName);
        public IEnumerable<string> GetGroups();
        public IEnumerable<string> GetUsersInGroup(string groupName);
        public bool GroupExists(string groupName);
    }
}
