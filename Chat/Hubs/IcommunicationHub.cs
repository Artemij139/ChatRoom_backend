using Chat.Models;

namespace Chat.Hubs
{
    public interface IcommunicationHub
    {   
        Task SendMessageAsync(string userName, string message);
        Task UpdateUsersAsync(IEnumerable<String> usersNames);
        
    }
}