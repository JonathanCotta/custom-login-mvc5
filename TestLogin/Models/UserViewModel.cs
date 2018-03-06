using System.ComponentModel.DataAnnotations;

namespace TestLogin.Models
{
    public class UserRegisterViewModel
    {
        [Required]       
        [StringLength(20,ErrorMessage = "O nome deve conter de 3 à 20 caracteres", MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]       
        [DataType(DataType.Password)]
        [StringLength(8,ErrorMessage = "A senha deve conter de 4 à 8 caracteres", MinimumLength = 4)]
        public string Password { get; set; }
        
        public bool Role { get; set; }
    }

    public class UserLoginViewModel
    {
        [Required]
        [Display(Name = "Nome")]
        [StringLength(20, ErrorMessage = "O nome deve conter de 3 à 20 caracteres", MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [StringLength(8, ErrorMessage = "A senha deve conter de 4 à 8 caracteres", MinimumLength = 4)]
        public string Password { get; set; }        
    }
}