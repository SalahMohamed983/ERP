using ApplicationLayer.Base;
using MediatR;
using System.Collections.Generic;
using ApplicationLayer.Features.Inventory.SuppliersCategory.Dtos;

namespace ApplicationLayer.Features.Inventory.SuppliersCategory.Queries.Models
{
    public class GetAllSuppliersCategoriesQuery : IRequest<Response<List<SuppliersCategoryDto>>>
    {
    }
}
