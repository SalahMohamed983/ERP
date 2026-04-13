using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Auth.Dtos;
using MediatR;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Models
{
    public class ResetPasswordCommand : IRequest<Response<bool>>
    {
        public ResetPasswordDto ResetPasswordDto { get; set; } = null!;
    }
}
