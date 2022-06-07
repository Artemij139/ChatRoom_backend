using Chat.Database;
using Chat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;


namespace Chat.Hubs
{

    [Authorize]
    public class chatHub : Hub<IcommunicationHub>
    {   
       
        public chatHub(Manager manager, ChatDbContex chatDb)
        {
            _manager = manager;
            _chatDb = chatDb;

        }

        public Manager _manager { get; }
        public ChatDbContex _chatDb { get; }


        public override async Task OnConnectedAsync()
        {
            var userName = Context.User?.Identity?.Name ?? "Anonymous";

            var connectionId = Context.ConnectionId;

            _manager.ConnectUser(userName, connectionId);

            await UpdateUsersAsync();

            await UpdateMessagesAsync();

            await base.OnConnectedAsync();



        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var isUserRemoved = _manager.DisconnectUser(Context.ConnectionId);
            await UpdateUsersAsync();
            await base.OnDisconnectedAsync(exception);
        }

        public Task UpdateUsersAsync()
        {
            var users = _manager.Users.Select(x=>x.Name).ToList();
            Clients.All.UpdateUsersAsync(users);
            return Task.CompletedTask;
        }

        public Task UpdateMessagesAsync()
        {
            var mess = _chatDb.messages.ToList();
            Clients.All.UpdateMessagesAsync(mess);
            return Task.CompletedTask;
        }


        [Authorize(Policy = "HasName")]
        public async Task SendMessageAsync(string userName, string message)
        {
            
            //change All to Others
            await Clients.All.SendMessageAsync(userName, message);
            var x = await _chatDb.messages.AddAsync(
                new Message
                {   
                    Id = Guid.NewGuid(),
                    userName = userName,
                    text = message
                });
            await _chatDb.SaveChangesAsync();
        }
    }
}
