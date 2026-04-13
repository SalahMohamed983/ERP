using ApplicationLayer.Base;
using MediatR;

namespace ApplicationLayer.Features.AuthAndPermissions.Email.Commands.Models
{
    public class SendPasswordResetCommand : IRequest<Response<bool>>
    {
        public string Email { get; set; } = null!;
        public string ResetLink { get; set; } = null!;
    }
}
