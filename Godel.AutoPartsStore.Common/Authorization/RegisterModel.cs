using System.ComponentModel.DataAnnotations;

namespace Godel.AutoPartsStore.Common.Authorization
{
    // класс представляющий модель регистрации
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is not specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "No password specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Incorrect password")]
        public string ConfirmPassword { get; set; }
    }
}
