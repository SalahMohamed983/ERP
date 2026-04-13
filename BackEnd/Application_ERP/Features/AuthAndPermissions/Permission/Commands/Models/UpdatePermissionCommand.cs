using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Permission.Dtos;
using MediatR;

namespace ApplicationLayer.Features.AuthAndPermissions.Permission.Commands.Models
{
    public class UpdatePermissionCommand : IRequest<Response<Unit>>
    {
        public PermissionDto Permission { get; set; } = null!;
    }
}
