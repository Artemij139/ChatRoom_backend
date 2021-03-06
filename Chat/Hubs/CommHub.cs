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

        //When client client, he get all history messages
        public Task UpdateMessagesAsync()
        {
            var mess = _chatDb.messages.ToList().OrderBy(x=>x.time);
            Clients.Caller.UpdateMessagesAsync(mess);
            return Task.CompletedTask;
        }

        public Task UpdateUsersAsync()
        {
            var users = _manager.Users.Select(x => x.Name).ToList();
            Clients.All.UpdateUsersAsync(users);
            return Task.CompletedTask;
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var isUserRemoved = _manager.DisconnectUser(Context.ConnectionId);
            if (isUserRemoved) { await UpdateUsersAsync(); }
            await base.OnDisconnectedAsync(exception);
        }

        
        [Authorize(Policy = "HasName")]
        public async Task SendMessageAsync(string userName, string message)
        {
            
            //change All to Others
            await Clients.All.SendMessageAsync(userName, message);
            await _chatDb.messages.AddAsync(
                new Message
                {   
                    Id = Guid.NewGuid(),
                    userName = userName,
                    text = message,
                    time = DateTime.Now     
                });
            await _chatDb.SaveChangesAsync();
        }
    }
}
