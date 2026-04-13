using System.Collections.Generic;
using Riok.Mapperly.Abstractions;
using DominLayer.Entites;
using ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Dtos;

namespace ERP_Business_Layer.Mapper.Genral_Settings.TreasuriesDeliveries
{
    [Mapper]
    public partial class TreasuriesDeliveryMapper
    {
        public static partial TreasuriesDeliveryDto ToDto(TreasuriesDelivery source);
        public static partial TreasuriesDelivery ToEntity(TreasuriesDeliveryDto source);
        public static partial List<TreasuriesDeliveryDto> ToDtoList(IEnumerable<TreasuriesDelivery> source);
        public static partial List<TreasuriesDelivery> ToEntityList(IEnumerable<TreasuriesDeliveryDto> source);
    }
}
