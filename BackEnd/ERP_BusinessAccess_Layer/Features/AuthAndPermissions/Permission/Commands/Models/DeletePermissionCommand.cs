using ApplicationLayer.Base;
using MediatR;

namespace ApplicationLayer.Features.AuthAndPermissions.Permission.Commands.Models
{
    public class DeletePermissionCommand : IRequest<Response<Unit>>
    {
        public int PermissionId { get; set; }
    }
}
