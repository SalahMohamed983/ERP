using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Dtos
{
    public class ConfirmEmailDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; } = string.Empty;
    }
}
