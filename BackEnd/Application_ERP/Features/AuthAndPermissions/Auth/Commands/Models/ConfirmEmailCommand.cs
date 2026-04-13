using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Auth.Dtos;
using MediatR;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Models
{
    public class ConfirmEmailCommand : IRequest<Response<bool>>
    {
        public ConfirmEmailDto ConfirmEmailDto { get; set; } = null!;
    }
}
