using ApplicationLayer.Base;
using MediatR;
using System;

namespace ApplicationLayer.Features.AuthAndPermissions.Role.Commands.Models
{
    public class DeleteRoleCommand : IRequest<Response<Unit>>
    {
        public Guid RoleId { get; set; }
    }
}
