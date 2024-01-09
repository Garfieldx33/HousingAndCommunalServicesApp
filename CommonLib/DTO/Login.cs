using System.ComponentModel.DataAnnotations;

namespace CommonLib.DTO
{
    public class Login
    {
        [Required(ErrorMessage = "Не указан Логин")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Введите валидный e-mail")]
        [Display(Name = "Логин")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Не указан Пароль")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Пароль должен быть не менее 4 символов")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public string ReturnURL { get; set; }

        public bool Remember { get; set; }
    }
}
