using System;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.DTOs
{
    public class ConversationCreateDTO
    {

        [Required]
        public string Message { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string SenderId { get; set; }
        [Required]
        public string RecieverId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

    }
}
