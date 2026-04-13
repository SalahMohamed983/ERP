using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Dtos
{
    public class RefreshTokenRequestDto
    {
        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; } = string.Empty;

        [Required(ErrorMessage = "Refresh token is required")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
