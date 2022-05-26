using System.ComponentModel.DataAnnotations;

namespace AuthorizeIdentityServer.ViewModels
{
    public class LoginViewModel

    {
        
        [Required(ErrorMessage = "Введите имя пользователя!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите пароль!")]
        [MinLength(6,ErrorMessage = "Пароль должен содержать не менее 6 символов!")]
        public string Password { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
        public string CustomError { get; set; } = string.Empty;

    }
}
