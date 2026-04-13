using System.Collections.Generic;
using Riok.Mapperly.Abstractions;
using DominLayer.Entites;
using ApplicationLayer.Features.GenralSettings.Admin.Dtos;

namespace ERP_Business_Layer.Mapper.Genral_Settings.Admin
{
    [Mapper]
    public partial class AdminPanelSettingMapper
    {
        public static partial AdminPanelSettingDto ToDto(AdminPanelSetting source);
        public static partial AdminPanelSetting ToEntity(AdminPanelSettingDto source);
        public static partial List<AdminPanelSettingDto> ToDtoList(IEnumerable<AdminPanelSetting> source);
        public static partial List<AdminPanelSetting> ToEntityList(IEnumerable<AdminPanelSettingDto> source);
    }
}
