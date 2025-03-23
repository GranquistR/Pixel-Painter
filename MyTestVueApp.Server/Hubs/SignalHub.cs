using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.SignalR;
using MyTestVueApp.Server.Interfaces;
using MyTestVueApp.Server.Entities;
using System.Drawing;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Logging;


namespace MyTestVueApp.Server.Hubs
{
    public class SignalHub : Hub
    {
        //key = groupname, value = list of users in the group
        private IConnectionManager _manager;
        private readonly ILogger<SignalHub> _logger;

        public SignalHub(IConnectionManager manager, ILogger<SignalHub> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public async Task JoinGroup(string groupName)
        {
            _manager.AddUser(groupName,Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");

            _logger.LogInformation("Member Count: " + _manager.GetGroup(groupName).GetMemberCount());
            if (_manager.GetGroup(groupName).GetMemberCount() > 1) 
            {
                _logger.LogInformation("Canvas" + _manager.GetGroup(groupName).GetPixelsAsList().ElementAt(0));
                await Clients.Client(Context.ConnectionId).SendAsync("Canvas", _manager.GetGroup(groupName).GetPixelsAsList());
            }

        }

        public async Task LeaveGroup(string groupName)
        {
            _manager.RemoveUser(groupName, Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }

        public async Task SendPixel(string room, string color, Coordinate coord)
        {
            _manager.PaintPixels(room, color, [coord]);
            Console.WriteLine("Room:" + room + "Color: " + color + "Coord: " + coord.ToString());
            await Clients.Group(room).SendAsync("ReceivePixel", color, coord);
        }

        public async Task SendPixels(string room, string color, Coordinate[] coords)
        {
            _manager.PaintPixels(room, color, coords);
            Console.WriteLine("Room:" + room + "Color: " + color + "Coord: " + coords.ToString());
            await Clients.Group(room).SendAsync("ReceivePixels", color, coords);
        }

        public async Task SendBucket(string room, string color, Coordinate coord)
        { // WARNING Not sending all painted pixels!
            await Clients.Group(room).SendAsync("ReceiveBucket", color, coord);
        }

        public async Task SendMessage(string user, string room, string message)
        {
            await Clients.Group(room).SendAsync(user, message);
        }

       
    }
}
