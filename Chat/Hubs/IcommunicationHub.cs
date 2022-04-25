using Chat.Models;

namespace Chat.Hubs
{
    public interface IcommunicationHub
    {   
        Task SendMessageAsync(string UserName, string Message);
        Task UpdateUsersAsync(IEnumerable<User> users);
        
    }
}