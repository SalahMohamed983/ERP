using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Auth.Dtos;
using MediatR;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Models
{
    public class ForgotPasswordCommand : IRequest<Response<bool>>
    {
        public ForgotPasswordDto ForgotPasswordDto { get; set; } = null!;
    }
}
