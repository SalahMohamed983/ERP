using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DominLayer.Entites.AuthAndPermissions
{
    public class AspNetRole : IdentityRole<Guid>
    {
        // Navigation
        public ICollection<RolePermission>? RolePermissions { get; set; }
    }
}
