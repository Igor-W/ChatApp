using System.ComponentModel.DataAnnotations;

namespace ChatApp.DTOs
{
    public class UserCreateByAdminDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
