using System.ComponentModel.DataAnnotations;

namespace TestLogin.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(20,MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(128)]
        public string Password { get; set; }
              
        public bool Role { get; set; }
    }
}