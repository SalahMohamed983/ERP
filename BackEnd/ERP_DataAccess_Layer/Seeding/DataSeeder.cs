using DominLayer.Entites.AuthAndPermissions;
using InfrastructureLayer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ResturantDataAccessLayer.Seeding
{
    public class DataSeeder : IDataSeeder
    {
        private readonly ERPContext _db;
        private readonly RoleManager<AspNetRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DataSeeder(ERPContext db, RoleManager<AspNetRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }
    
        public async Task SeedAsync()
        {
            await _db.Database.MigrateAsync();

            // Roles
            var roles = new[] { "User", "Admin" };
            foreach (var roleName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    var role = new AspNetRole { Id = Guid.NewGuid(), Name = roleName, NormalizedName = roleName.ToUpperInvariant() };
                    await _roleManager.CreateAsync(role);
                }
            }

            // Admin user
            var adminEmail = "admin@local";
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FullName = "System Administrator",
                    CreatedDate = DateTime.UtcNow
                };
                var result = await _userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Other users
            var userEmails = new[] { "user1@local", "user2@local", "user3@local" };
            var createdUsers = new List<ApplicationUser> { adminUser };
            foreach (var email in userEmails)
            {
                var u = await _userManager.FindByEmailAsync(email);
                if (u == null)
                {
                    u = new ApplicationUser
                    {
                        Id = Guid.NewGuid(),
                        UserName = email,
                        Email = email,
                        EmailConfirmed = true,
                        FullName = email.Split('@')[0],
                        CreatedDate = DateTime.UtcNow
                    };
                    var r = await _userManager.CreateAsync(u, "User@1234");
                    if (r.Succeeded)
                        await _userManager.AddToRoleAsync(u, "User");
                }
                createdUsers.Add(u);
            }

            // Permissions and role-permissions
            if (!await _db.Permissions.AnyAsync())
            {
                var perms = new List<Permission>
                    {
                        new Permission { Code = "VIEW_ORDERS", Description = "View orders" },
                        new Permission { Code = "MANAGE_MENUS", Description = "Manage menu items and categories" },
                        new Permission { Code = "MANAGE_RESERVATIONS", Description = "Approve or reject reservations" }
                    };
                await _db.Permissions.AddRangeAsync(perms);
                await _db.SaveChangesAsync();

                var adminRole = await _roleManager.FindByNameAsync("Admin");
                var userRole = await _roleManager.FindByNameAsync("User");
                var savedPerms = await _db.Permissions.ToListAsync();

                var rolePerms = new List<RolePermission>();
                foreach (var p in savedPerms)
                    rolePerms.Add(new RolePermission { RoleId = adminRole.Id, PermissionId = p.Id });

                var view = savedPerms.FirstOrDefault(p => p.Code == "VIEW_ORDERS");
                if (view != null && userRole != null)
                    rolePerms.Add(new RolePermission { RoleId = userRole.Id, PermissionId = view.Id });

                await _db.RolePermissions.AddRangeAsync(rolePerms);
                await _db.SaveChangesAsync();
            }

            // Ensure persisted
            await _db.SaveChangesAsync();
        }
    }
}
