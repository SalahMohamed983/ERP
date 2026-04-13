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
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, Response<Unit>>
    {
        private readonly RoleManager<AspNetRole> _roleManager;
        private readonly ResponseHandler _responseHandler;
        public UpdateRoleHandler(RoleManager<AspNetRole> roleManager, ResponseHandler responseHandler) { _roleManager = roleManager; _responseHandler = responseHandler; }

        public async Task<Response<Unit>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Role;
            var role = await _roleManager.FindByIdAsync(dto.Id.ToString());
            if (role == null) return _responseHandler.NotFound<Unit>("Role not found.");

            role.Name = dto.Name;
            role.NormalizedName = dto.Name?.ToUpperInvariant();
            var res = await _roleManager.UpdateAsync(role);
            if (!res.Succeeded) throw new InvalidOperationException($"Failed to update role: {string.Join(',', res.Errors.Select(e=>e.Description))}");
            return _responseHandler.Success(Unit.Value);
        }
    }
}
