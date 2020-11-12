using System;

namespace ChatApp.DTOs
{
    public class ConversationReadDTO
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string SenderId { get; set; }
        public string RecieverId { get; set; }
        public DateTime CreatedAt { get; set; }



    }
}
