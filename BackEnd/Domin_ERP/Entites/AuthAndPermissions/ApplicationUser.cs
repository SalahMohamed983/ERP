using Microsoft.AspNetCore.Identity;
using System;

namespace DominLayer.Entites.AuthAndPermissions
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FullName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int TokenVersion { get; set; }

        // Navigation
        public ICollection<RefreshToken>? RefreshTokens { get; set; }
    }
}
