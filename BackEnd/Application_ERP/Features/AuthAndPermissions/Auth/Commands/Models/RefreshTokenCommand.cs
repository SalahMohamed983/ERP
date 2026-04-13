using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Auth.Dtos;
using MediatR;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<AuthResponseDto?>>
    {
        public RefreshTokenRequestDto RefreshTokenRequestDto { get; set; } = null!;
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }
}
