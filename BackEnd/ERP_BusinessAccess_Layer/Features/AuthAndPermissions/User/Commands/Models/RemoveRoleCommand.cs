using ApplicationLayer.Base;
using MediatR;
using System;

namespace ApplicationLayer.Features.AuthAndPermissions.User.Commands.Models
{
    public class RemoveRoleCommand : IRequest<Response<Unit>>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
