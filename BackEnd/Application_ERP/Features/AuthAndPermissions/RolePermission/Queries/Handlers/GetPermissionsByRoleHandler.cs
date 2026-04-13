using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Permission.Dtos;
using ApplicationLayer.Features.AuthAndPermissions.RolePermission.Queries.Models;
using ApplicationLayer.Mapper.AuthAndPermission;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.AuthAndPermissions.RolePermission.Queries.Handlers
{
    public class GetPermissionsByRoleHandler : IRequestHandler<GetPermissionsByRoleQuery, Response<IEnumerable<PermissionDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public GetPermissionsByRoleHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<IEnumerable<PermissionDto>>> Handle(GetPermissionsByRoleQuery request, CancellationToken cancellationToken)
        {
            var permissionIds = _uow.RolePermissions.Query().Where(rp => rp.RoleId == request.RoleId).Select(rp => rp.PermissionId).ToList();
            var allPermissions = await _uow.Permissions.GetAllAsync();
            var permissions = allPermissions.Where(p => permissionIds.Contains(p.Id)).Select(p => PermissionMapper.Map(p)).ToList();
            return _responseHandler.Success<IEnumerable<PermissionDto>>(permissions);
        }
    }
}
