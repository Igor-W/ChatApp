using System;

namespace ChatApp.DTOs
{
    public class UserReadByAdminDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileImage { get; set; }
    }
}
