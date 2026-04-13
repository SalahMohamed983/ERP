using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Role.Dtos;
using MediatR;
using System.Collections.Generic;

namespace ApplicationLayer.Features.AuthAndPermissions.Role.Queries.Models
{
    public class GetAllRolesQuery : IRequest<Response<IEnumerable<RoleDto>>> { }
}
