using Riok.Mapperly.Abstractions;
using DominLayer.Entites.AuthAndPermissions;
using ApplicationLayer.Features.AuthAndPermissions.Permission.Dtos;
using System.Collections.Generic;

namespace ApplicationLayer.Mapper.AuthAndPermission
{
    [Mapper]
    public static partial class PermissionMapper
    {
        public static partial Permission Map(PermissionDto dto);
        public static partial IEnumerable<Permission> Map(IEnumerable<PermissionDto> dtos);
        ////////////////////////////////////Permission To PermissionDto////////////////////////////////////////////
        public static partial PermissionDto Map(Permission permission);
        public static partial IEnumerable<PermissionDto> Map(IEnumerable<Permission> permissions);
    }
}
