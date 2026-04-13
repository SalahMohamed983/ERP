using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.Role.Commands.Models;
using DominLayer.Entites.AuthAndPermissions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.AuthAndPermissions.Role.Commands.Handlers
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, Response<Unit>>
    {
        private readonly RoleManager<AspNetRole> _roleManager;
        private readonly ResponseHandler _responseHandler;
        public DeleteRoleHandler(RoleManager<AspNetRole> roleManager, ResponseHandler responseHandler) { _roleManager = roleManager; _responseHandler = responseHandler; }

        public async Task<Response<Unit>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role == null) return _responseHandler.NotFound<Unit>("Role not found.");
            var res = await _roleManager.DeleteAsync(role);
            if (!res.Succeeded) throw new InvalidOperationException($"Failed to delete role: {string.Join(',', res.Errors.Select(e=>e.Description))}");
            return _responseHandler.Deleted<Unit>();
        }
    }
}
