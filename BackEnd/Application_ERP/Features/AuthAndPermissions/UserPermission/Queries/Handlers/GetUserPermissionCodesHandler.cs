using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.UserPermission.Queries.Models;
using ApplicationLayer.RepoInterfaces;
using DominLayer.Entites.AuthAndPermissions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.AuthAndPermissions.UserPermission.Queries.Handlers
{
    public class GetUserPermissionCodesHandler : IRequestHandler<GetUserPermissionCodesQuery, Response<IEnumerable<string>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public GetUserPermissionCodesHandler(UserManager<ApplicationUser> userManager, IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _userManager = userManager;
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<IEnumerable<string>>> Handle(GetUserPermissionCodesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return _responseHandler.Success(Enumerable.Empty<string>());

            var userRoles = await _userManager.GetRolesAsync(user);
            if (!userRoles.Any()) return _responseHandler.Success(Enumerable.Empty<string>());

            var roles = await _uow.Roles.Query().Where(r => userRoles.Contains(r.Name!)).Select(r => r.Id).ToListAsync();
            if (!roles.Any()) return _responseHandler.Success(Enumerable.Empty<string>());

            var permissionIds = await _uow.RolePermissions.Query().Where(rp => roles.Contains(rp.RoleId)).Select(rp => rp.PermissionId).Distinct().ToListAsync();
            if (!permissionIds.Any()) return _responseHandler.Success(Enumerable.Empty<string>());

            var permissions = await _uow.Permissions.Query().Where(p => permissionIds.Contains(p.Id) &&
            !string.IsNullOrEmpty(p.Code)).Select(p => p.Code!).ToListAsync();

            return _responseHandler.Success(permissions.Distinct());
        }
    }
}
