using System;

namespace ChatApp.DTOs
{
    public class UserReadDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string ProfileImage { get; set; }
    }
}
