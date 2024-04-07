using System.ComponentModel.DataAnnotations;

namespace Bank.Domain.DTO.Login
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}
