using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Permission.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace ApplicationLayer.Features.AuthAndPermissions.RolePermission.Queries.Models
{
    public class GetPermissionsByRoleQuery : IRequest<Response<IEnumerable<PermissionDto>>>
    {
        public Guid RoleId { get; set; }
    }
}
