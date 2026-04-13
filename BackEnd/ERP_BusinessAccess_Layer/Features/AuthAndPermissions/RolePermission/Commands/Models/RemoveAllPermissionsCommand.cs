using ApplicationLayer.Base;
using MediatR;
using System;

namespace ApplicationLayer.Features.AuthAndPermissions.RolePermission.Commands.Models
{
    public class RemoveAllPermissionsCommand : IRequest<Response<Unit>>
    {
        public Guid RoleId { get; set; }
    }
}
