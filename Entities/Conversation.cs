using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Entities
{
    public class Conversation
    {
        [Key]
        [Column]
        public int Id { get; set; }
        [Column]
        [Required]
        public string Message { get; set; }
        [Column]
        [Required]
        public string Status { get; set; }
        [Column]

        public string SenderId { get; set; }
        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; }
        [Column]

        public string RecieverId { get; set; }
        [ForeignKey("RecieverId")]
        public virtual User Reciever { get; set; }
        [Column]
        [Required]
        public DateTime CreatedAt { get; set; }

    }
}
