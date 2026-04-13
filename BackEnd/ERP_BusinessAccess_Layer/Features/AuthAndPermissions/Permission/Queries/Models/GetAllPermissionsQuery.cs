using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Permission.Dtos;
using MediatR;
using System.Collections.Generic;

namespace ApplicationLayer.Features.AuthAndPermissions.Permission.Queries.Models
{
    public class GetAllPermissionsQuery : IRequest<Response<IEnumerable<PermissionDto>>>
    {
    }
}
