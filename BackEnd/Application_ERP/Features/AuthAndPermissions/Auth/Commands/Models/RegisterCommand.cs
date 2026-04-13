using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Auth.Dtos;
using MediatR;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Models
{
    public class RegisterCommand : IRequest<Response<bool>>
    {
        public RegisterDto RegisterDto { get; set; } = null!;
    }
}
