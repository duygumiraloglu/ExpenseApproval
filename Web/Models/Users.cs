using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Users
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string PasswordHash { get; set; }

        public string? FullName { get; set; }

        [StringLength(50)]
        public string? Email { get; set; }

        [StringLength(50)]
        public string? Role { get; set; }
    }
}
