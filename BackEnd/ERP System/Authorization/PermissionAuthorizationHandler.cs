using ApplicationLayer.RepoInterfaces;
using DominLayer.Entites.AuthAndPermissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Resturant.Authorization
{
    /// <summary>
    /// Authorization handler that checks if the user has the required permission through their roles
    /// </summary>
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _uow;

        public PermissionAuthorizationHandler(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork uow)
        {
            _userManager = userManager;
            _uow = uow;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            // If user is not authenticated, fail
            if (!context.User.Identity?.IsAuthenticated ?? true)
            {
                return;
            }

            // Get user ID from claims
            var userIdClaim = context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return;
            }

            // Get user from database
            var user = await _userManager.FindByIdAsync(userIdClaim);
            if (user == null )
            {
                return;
            }

            // Get user roles
            var userRoles = await _userManager.GetRolesAsync(user);
            if (!userRoles.Any())
            {
                return;
            }

            // Get role IDs
            var roles = await _uow.Roles.Query()
                .Where(r => userRoles.Contains(r.Name!))
                .Select(r => r.Id)
                .ToListAsync();

            if (!roles.Any())
            {
                return;
            }

            // Check if any of the user's roles has the required permission
            // Use ToLower() for case-insensitive comparison (EF Core can translate this to SQL)
            var permissionCodeLower = requirement.PermissionCode.ToLower();
            var permission = await _uow.Permissions.Query()
                .FirstOrDefaultAsync(p => p.Code != null && 
                    p.Code.ToLower() == permissionCodeLower);

            if (permission == null)
            {
                return; // Permission doesn't exist
            }

            var hasPermission = await _uow.RolePermissions.Query()
                .AnyAsync(rp => roles.Contains(rp.RoleId) && rp.PermissionId == permission.Id);

            if (hasPermission)
            {
                context.Succeed(requirement);
            }
        }
    }
}