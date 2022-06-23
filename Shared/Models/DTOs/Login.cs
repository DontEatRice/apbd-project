using System.ComponentModel.DataAnnotations;

namespace blazor_project.Shared.Models.DTOs
{
    public class Login
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }
    }
}