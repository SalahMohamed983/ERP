using System;
using System.Collections.Generic;

namespace ApplicationLayer.Features.AuthAndPermissions.User.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? ImageUser { get; set; }
        public string? PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<string>? Roles { get; set; }
    }
}
