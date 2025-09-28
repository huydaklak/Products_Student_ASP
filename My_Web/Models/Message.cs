using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_Web.Models
{
    [Table("messages")]
    public class Message
    {
        public int MessageID { get; set; }
        public int UserID { get; set; }
        public User? User { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

    }
}
