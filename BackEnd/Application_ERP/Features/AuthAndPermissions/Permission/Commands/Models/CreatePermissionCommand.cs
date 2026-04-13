using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Permission.Dtos;
using MediatR;

namespace ApplicationLayer.Features.AuthAndPermissions.Permission.Commands.Models
{
    public class CreatePermissionCommand : IRequest<Response<int>>
    {
        public PermissionDto Permission { get; set; } = null!;
    }
}
