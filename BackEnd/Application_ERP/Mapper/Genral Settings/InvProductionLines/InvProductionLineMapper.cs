using System.Collections.Generic;
using Riok.Mapperly.Abstractions;
using DominLayer.Entites;
using ApplicationLayer.Features.GenralSettings.InvProductionLines.Dtos;

namespace ERP_Business_Layer.Mapper.Genral_Settings.InvProductionLines
{
    [Mapper]
    public partial class InvProductionLineMapper
    {
        public static partial InvProductionLineDto ToDto(InvProductionLine source);
        public static partial InvProductionLine ToEntity(InvProductionLineDto source);
        public static partial List<SmallInvProductionLineDto> ToDtoList(IEnumerable<InvProductionLine> source);
    }
}
