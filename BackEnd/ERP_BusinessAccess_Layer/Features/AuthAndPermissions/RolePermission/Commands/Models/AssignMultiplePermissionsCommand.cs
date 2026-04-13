using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.RolePermission.Dtos;
using MediatR;

namespace ApplicationLayer.Features.AuthAndPermissions.RolePermission.Commands.Models
{
    public class AssignMultiplePermissionsCommand : IRequest<Response<Unit>>
    {
        public AssignMultiplePermissionsDto Dto { get; set; } = null!;
    }
}
