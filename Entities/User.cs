using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Entities
{
    public class User : IdentityUser
    {
        [Column]
        [Required]
        [MaxLength(21)]
        public string FirstName { get; set; }

        [Column]
        [Required]
        [MaxLength(21)]
        public string LastName { get; set; }

        [Column]
        [Required]
        public bool Gender { get; set; }

        [Column]
        [Required]
        public string Position { get; set; }

        [Column]
        [Required]
        public string ProfileImage { get; set; }
    }
}
