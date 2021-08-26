using System.ComponentModel.DataAnnotations;

namespace Godel.AutoPartsStore.Common.Authorization
{
    // класс представляющий модель логина
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is not specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "No password specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
