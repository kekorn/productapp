using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class MyUser
    {
        [Key]
        public int UserID { get; set; }
        [Required, StringLength(50)]
        public string Username { get; set; }
        [Required, StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Role { get; set; } = "User";
    }
}
