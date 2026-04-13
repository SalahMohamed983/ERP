using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.Inventory.SuppliersCategory.Dtos;

namespace ApplicationLayer.Features.Inventory.SuppliersCategory.Commands.Models
{
    public class UpdateSuppliersCategoryCommand : IRequest<Response<Unit>>
    {
        public SuppliersCategoryDto Dto { get; set; } = null!;
    }
}
