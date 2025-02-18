using Microsoft.AspNetCore.SignalR;


namespace MyTestVueApp.Server.Hubs
{
    public class SignalHub : Hub
    {
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }

        public async Task SendMessage(string user, string room, string message)
        {
            await Clients.Group(room).SendAsync(user, message);
        }
    }
}
