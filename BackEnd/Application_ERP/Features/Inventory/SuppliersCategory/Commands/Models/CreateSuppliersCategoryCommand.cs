using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.Inventory.SuppliersCategory.Dtos;

namespace ApplicationLayer.Features.Inventory.SuppliersCategory.Commands.Models
{
    public class CreateSuppliersCategoryCommand : IRequest<Response<int>>
    {
        public SuppliersCategoryDto Dto { get; set; } = null!;
    }
}
