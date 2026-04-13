using System;

namespace DominLayer.Entites.AuthAndPermissions
{
    public class RolePermission
    {
        public Guid RoleId { get; set; }
        public int PermissionId { get; set; }

        // Navigation
        public AspNetRole? Role { get; set; }
        public Permission? Permission { get; set; }
    }
}
