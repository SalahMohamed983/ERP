using System.Collections.Generic;
using Riok.Mapperly.Abstractions;
using DominLayer.Entites;
using ApplicationLayer.Features.Inventory.Suppliers.Dtos;
using ApplicationLayer.Features.Inventory.SuppliersCategory.Dtos;

namespace ERP_Business_Layer.Mapper.Inventory_Settings
{
    [Mapper]
    public partial class SuuplierMapper
    {
        public static partial SuuplierDto ToDto(Suuplier source);
        public static partial Suuplier ToEntity(SuuplierDto source);
        public static partial List<SuuplierDto> ToDtoList(IEnumerable<Suuplier> source);
        public static partial List<Suuplier> ToEntityList(IEnumerable<SuuplierDto> source);
    }
    [Mapper]
    public partial class SuppliersCategoryMapper
    {
        public static partial SuppliersCategoryDto ToDto(SuppliersCategory source);
        public static partial SuppliersCategory ToEntity(SuppliersCategoryDto source);
        public static partial List<SuppliersCategoryDto> ToDtoList(IEnumerable<SuppliersCategory> source);
    }
}
