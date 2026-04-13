using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Dtos
{
    public class ResendConfirmationEmailDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
    }
}
