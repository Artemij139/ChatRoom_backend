using Chat.Models;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Hubs
{
    public class CommHub : Hub<IcommunicationHub>
    {
        public CommHub(Manager manager)
        {
            _manager = manager;
        }

        public Manager _manager { get; }

        public override Task OnConnectedAsync()
        {
            var userName = Context.User?.Identity?.Name ?? "Anonymous";
            var connectionId = Context.ConnectionId;
            
             _manager.ConnectUser(userName, connectionId);
            return base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _manager.DisconnectUser(Context.ConnectionId);
            await UpdateUsersAsync();
            await base.OnDisconnectedAsync(exception);
        }

        public Task UpdateUsersAsync()
        {
            var users = _manager.Users.ToList();
            Clients.All.UpdateUsersAsync(users);
            return Task.CompletedTask;
        }

        public async Task SendMessageAsync(string userName, string message)
        {   
            //change All to Others
            await Clients.All.SendMessageAsync(userName, message);
        }
    }
}
