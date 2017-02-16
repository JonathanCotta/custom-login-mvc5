using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestLogin.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(20,MinimumLength = 4, ErrorMessage = "Minimo de 4 caracteres")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(128,MinimumLength = 4 , ErrorMessage = "Minimo de 4 caracteres e máximo de 12")]
        public string Password { get; set; }
              
        public bool Role { get; set; }
    }
}