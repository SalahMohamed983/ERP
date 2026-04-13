using ApplicationLayer.Features.AuthAndPermissions.Auth.Dtos;
using ApplicationLayer.Features.AuthAndPermissions.User.Dtos;
using DominLayer.Entites.AuthAndPermissions;
using Riok.Mapperly.Abstractions;
using System.Collections.Generic;

namespace ApplicationLayer.Mapper.AuthAndPermission
{
    [Mapper]
    public static partial class UserMapper
    {
        public static partial UserDto Map(ApplicationUser user);
        public static partial IEnumerable<UserDto> Map(IEnumerable<ApplicationUser> users);

    }
}
