using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Role.Dtos;
using MediatR;

namespace ApplicationLayer.Features.AuthAndPermissions.Role.Commands.Models
{
    public class UpdateRoleCommand : IRequest<Response<Unit>>
    {
        public RoleDto Role { get; set; } = null!;
    }
}
