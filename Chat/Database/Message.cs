using System.ComponentModel.DataAnnotations;

namespace Chat.Database
{
    public class Message
    {   
        [Key]
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string userName { get; set; }

        [MaxLength(1000)]
        public string text { get; set; }

        public DateTime time { get; set; }
    }
}
