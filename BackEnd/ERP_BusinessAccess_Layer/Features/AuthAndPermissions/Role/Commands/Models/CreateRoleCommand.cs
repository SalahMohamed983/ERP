using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Role.Dtos;
using MediatR;

namespace ApplicationLayer.Features.AuthAndPermissions.Role.Commands.Models
{
    public class CreateRoleCommand : IRequest<Response<Guid>>
    {
        public RoleDto Role { get; set; } = null!;
    }
}
