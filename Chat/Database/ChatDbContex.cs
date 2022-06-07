using Microsoft.EntityFrameworkCore;

namespace Chat.Database
{
    public class ChatDbContex : DbContext
    {
        public ChatDbContex(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Message> messages{ get; set; }
    }

}
