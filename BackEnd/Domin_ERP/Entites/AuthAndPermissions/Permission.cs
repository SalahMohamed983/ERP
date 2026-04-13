using System;

namespace DominLayer.Entites.AuthAndPermissions
{
    public class Permission
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
    }
}
