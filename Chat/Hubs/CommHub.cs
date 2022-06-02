using Chat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;


namespace Chat.Hubs
{

    [Authorize]
    public class chatHub : Hub<IcommunicationHub>
    {   
       
        public chatHub(Manager manager)
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
            var isUserRemoved = _manager.DisconnectUser(Context.ConnectionId);
            await UpdateUsersAsync();
            await base.OnDisconnectedAsync(exception);
        }

        public Task UpdateUsersAsync()
        {
            var users = _manager.Users.ToList();
            Clients.All.UpdateUsersAsync(users);
            return Task.CompletedTask;
        }


        [Authorize(Policy = "HasName")]
        public async Task SendMessageAsync(string userName, string message)
        {
            var x = Context.User;
            //change All to Others
            await Clients.All.SendMessageAsync(userName, message);
        }
    }
}
