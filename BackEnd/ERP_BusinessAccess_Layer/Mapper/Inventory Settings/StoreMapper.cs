using ApplicationLayer.Features.Inventory.InvItemcard.Dtos;
using ApplicationLayer.Features.Inventory.InvItemcardCategory.Dtos;
using ApplicationLayer.Features.Inventory.InvUom.Dtos;
using ApplicationLayer.Features.Inventory.Stores.Dtos;
using DominLayer.Entites;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Business_Layer.Mapper.Inventory_Settings
{

    [Mapper]
    public partial class StoreMapper
    {
        // Admin
        public static partial StoreDto ToDto(Store Store);
        public static partial Store ToEntity(StoreDto StoreDto);
        public static partial List<StoreDto> ToDtoList(IEnumerable<Store> source);
        public static partial List<Store> ToEntityList(IEnumerable<StoreDto> source);

    }
    [Mapper]
    public partial class InvUomMapper
    {
        public static partial InvUomDto ToDto(InvUom source);
        public static partial InvUom ToEntity(InvUomDto source);
        public static partial List<InvUomDto> ToDtoList(IEnumerable<InvUom> source);
        public static partial List<InvUom> ToEntityList(IEnumerable<InvUomDto> source);
    }
    [Mapper]
    public partial class InvItemcardCategoryMapper
    {
        public static partial InvItemcardCategoryDto ToDto(InvItemcardCategory source);
        public static partial InvItemcardCategory ToEntity(InvItemcardCategoryDto source);
        public static partial List<InvItemcardCategoryDto> ToDtoList(IEnumerable<InvItemcardCategory> source);
        public static partial List<InvItemcardCategory> ToEntityList(IEnumerable<InvItemcardCategoryDto> source);
    }
    [Mapper]
    public partial class InvItemcardMapper
    {
        public static partial InvItemcardDto ToDto(InvItemcard source);
        public static partial InvItemcard ToEntity(InvItemcardDto source);
        public static partial List<InvItemcardDto> ToDtoList(IEnumerable<InvItemcard> source);
        public static partial List<InvItemcard> ToEntityList(IEnumerable<InvItemcardDto> source);
    }
}
