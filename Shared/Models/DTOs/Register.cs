using System.ComponentModel.DataAnnotations;

namespace blazor_project.Shared.Models.DTOs
{
    public class Register
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match!")]
        public string PasswordConfirm { get; set; } = null!;
    }
}