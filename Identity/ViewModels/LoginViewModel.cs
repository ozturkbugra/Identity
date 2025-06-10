using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels
{
    public class LoginViewModel
    {

        [Required]
        public string Email { get; set; } = null!;


        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; } = null!;

        public bool RememberMe { get; set; } = true;

    }
}
