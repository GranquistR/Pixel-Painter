
using Microsoft.AspNetCore.SignalR;
using MyTestVueApp.Server.Interfaces;
using MyTestVueApp.Server.Entities;


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

        public async Task CreateOrJoinGroup(string groupName, Artist artist, string[][][] canvas, int canvasSize, string backgroundColor)
        {
            _logger.LogInformation("GroupName: " + groupName + " GroupExists: " + _manager.GroupExists(groupName));
            if (_manager.GroupExists(groupName))
            {
                _logger.LogInformation("Joining Group!");
                await JoinGroup(groupName, artist);
            } else
            {
                _logger.LogInformation("Creating Group!");
                await CreateGroup(groupName, artist, canvas, canvasSize, backgroundColor);
            }
        }

        public async Task JoinGroup(string groupName, Artist artist)
        {
            _manager.AddUser(Context.ConnectionId, artist, groupName); 
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("Send", $"{artist.name} has joined the group {groupName}.");
           
            await Clients.Client(Context.ConnectionId).SendAsync("GroupConfig", _manager.GetGroup(groupName).CanvasSize, _manager.GetGroup(groupName).BackgroundColor, _manager.GetGroup(groupName).GetPixelsAsList());
            await Clients.Client(Context.ConnectionId).SendAsync("Members", _manager.GetGroup(groupName).CurrentMembers);
            
            await Clients.Group(groupName).SendAsync("NewMember", artist);
        }


        public async Task CreateGroup(string groupName, Artist artist, string[][][] canvas, int canvasSize, string backgroundColor)
        { 
            _manager.AddGroup(groupName,canvas,canvasSize,backgroundColor);
            _manager.AddUser(Context.ConnectionId, artist, groupName);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("Send", $"{artist.name} has joined the group {groupName}.");
        }

        public async Task LeaveGroup(string groupName, Artist member)
        {
            _manager.RemoveUserFromGroup(Context.ConnectionId, member, groupName);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("Send", $"{member.name} has left the group {groupName}.");
        }

        public async Task SendPixels(string room, int layer, string color, Coordinate[] coords)
        {
            _manager.PaintPixels(room, layer, color, coords);
            await Clients.Group(room).SendAsync("ReceivePixels", layer, color, coords);
        }

        public async Task SendMessage(string user, string room, string message)
        {
            await Clients.Group(room).SendAsync(user, message);
        }

        public async Task ChangeBackgroundColor(string groupName, string backgroundColor)
        {
            _manager.GetGroup(groupName).BackgroundColor = backgroundColor;
            await Clients.Group(groupName).SendAsync("BackgroundColor", backgroundColor);
        }

        public async Task GetGroupMembers(string groupName)
        {
            await Clients.Group(groupName).SendAsync("GroupMembers", _manager.GetGroup(groupName).CurrentMembers);
        }

        public async Task GetContributingArtists(string groupName)
        {
            await Clients.Group(groupName).SendAsync("ContributingArtists", _manager.GetGroup(groupName).MemberRecord);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (exception != null)
            {
                _manager.RemoveUserFromAllGroups(Context.ConnectionId);
                _logger.LogError($"Error, Disconnected: {exception.Message}");
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
