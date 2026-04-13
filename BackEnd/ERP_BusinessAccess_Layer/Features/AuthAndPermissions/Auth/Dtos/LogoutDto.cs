using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Dtos
{
    public class LogoutDto
    {
        [Required(ErrorMessage = "Refresh token is required")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
