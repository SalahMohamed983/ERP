using System.Collections.Generic;
using Riok.Mapperly.Abstractions;
using DominLayer.Entites;
using ApplicationLayer.Features.GenralSettings.Treasuries.Dtos;

namespace ERP_Business_Layer.Mapper.Genral_Settings.Treasuries
{
    [Mapper]
    public partial class TreasuryMapper
    {
        public static partial TreasuryDto ToDto(Treasury source);
        public static partial Treasury ToEntity(TreasuryDto source);
        public static partial List<TreasuryDto> ToDtoList(IEnumerable<Treasury> source);
        public static partial List<Treasury> ToEntityList(IEnumerable<TreasuryDto> source);
    }
}
