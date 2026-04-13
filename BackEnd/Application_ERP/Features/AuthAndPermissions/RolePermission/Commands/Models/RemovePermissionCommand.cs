using ApplicationLayer.Base;
using MediatR;

namespace ApplicationLayer.Features.AuthAndPermissions.RolePermission.Commands.Models
{
    public class RemovePermissionCommand : IRequest<Response<Unit>>
    {
        public Guid RoleId { get; set; }
        public int PermissionId { get; set; }
    }
}
