using Chat.Models;

namespace Chat.Models
{
    public class User
    {
        internal readonly string Name = null!;
        internal readonly List<Connection> _chatConnections;

        public User(string name)
        {
            Name = name;
            _chatConnections = new List<Connection>();
        }

        public void AddConnection(string connectionId)
        {
            if(connectionId == null)
            {
                throw new ArgumentNullException(nameof(connectionId));  
            }

            var connection = new Connection()
            {
                ConnectionId = connectionId,
                ConnnectedAt = DateTime.UtcNow
            };

            _chatConnections.Add(connection);
        }

        
        public void RemoveConnection(string connectionId)
        {
            if (connectionId == null)
            {
                throw new ArgumentNullException(nameof(connectionId));
            }
            var connection = _chatConnections.SingleOrDefault(x => x.ConnectionId == connectionId);
            if(connection == null)
            {
                return;
            }  
            _chatConnections.Remove(connection);   
        }

    }
}
