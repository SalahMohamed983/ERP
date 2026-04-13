using ApplicationLayer.Base;
using MediatR;

namespace ApplicationLayer.Features.AuthAndPermissions.Email.Commands.Models
{
    public class SendEmailConfirmationCommand : IRequest<Response<bool>>
    {
        public string Email { get; set; } = null!;
        public string ConfirmationLink { get; set; } = null!;
    }
}
