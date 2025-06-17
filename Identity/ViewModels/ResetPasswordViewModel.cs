using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string? Token { get; set; }

        [Required]
        public string? Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Parolalar uyuşmuyor")]
        public string? ConfirmPassword { get; set; }
    }
}
