using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Auth.Dtos;
using MediatR;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Models
{
    public class ResendConfirmationEmailCommand : IRequest<Response<bool>>
    {
        public ResendConfirmationEmailDto ResendConfirmationEmailDto { get; set; } = null!;
    }
}
