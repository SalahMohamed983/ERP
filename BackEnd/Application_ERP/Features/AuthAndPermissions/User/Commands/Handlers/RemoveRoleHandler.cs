using ApplicationLayer.Base;
using ApplicationLayer.Features.AuthAndPermissions.User.Commands.Models;
using DominLayer.Entites.AuthAndPermissions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.AuthAndPermissions.User.Commands.Handlers
{
    public class RemoveRoleHandler : IRequestHandler<RemoveRoleCommand, Response<Unit>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<AspNetRole> _roleManager;
        private readonly ResponseHandler _responseHandler;

        public RemoveRoleHandler(UserManager<ApplicationUser> userManager, RoleManager<AspNetRole> roleManager, ResponseHandler responseHandler)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Unit>> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) throw new InvalidOperationException($"User with ID {request.UserId} not found.");

            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role == null) throw new InvalidOperationException($"Role with ID {request.RoleId} not found.");

            var res = await _userManager.RemoveFromRoleAsync(user, role.Name!);
            if (!res.Succeeded) throw new InvalidOperationException($"Failed to remove role: {string.Join(',', res.Errors.Select(e=>e.Description))}");

            return _responseHandler.Success(Unit.Value);
        }
    }
}
