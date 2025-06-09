using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels
{
    public class CreateViewModel
    {
        
        [Required]
        public string? FullName { get; set; }
        
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Parolalar uyuşmuyor")]
        public string? ConfirmPassword { get; set; }
    }
}
