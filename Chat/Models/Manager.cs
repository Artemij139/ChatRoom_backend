namespace Chat.Models
{
    public class Manager
    {
        internal readonly List<User> Users = new();
        public void ConnectUser(string UserName, string connectionId)
        {
            var userAlreadyExist = GetConnectedUserByName(UserName);

            if (userAlreadyExist != null)
            {
                userAlreadyExist.AddConnection(connectionId);
               
            }
            else
            {
                var user = new User(UserName);
                user.AddConnection(connectionId);
                Users.Add(user);
            }

        }

        public void DisconnectUser( string connectionId)
        {
            var userExist = GetConnectedUserById(connectionId);
            if(userExist == null)
            {
                return;
            }

            userExist.RemoveConnection(connectionId);

            if (userExist._chatConnections.Count == 0)
            {
                Users.Remove(userExist);
            }

        }

        private User? GetConnectedUserById(string connectionId)
        {
           return  Users.FirstOrDefault(x => x._chatConnections.Select(c => c.ConnectionId)
                .Contains(connectionId));
        }


        private User? GetConnectedUserByName(string userName)
        {
            return Users.FirstOrDefault(x => string.Equals
            ( x.Name, userName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
