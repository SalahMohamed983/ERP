using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.Inventory.Suppliers.Dtos;

namespace ApplicationLayer.Features.Inventory.Suppliers.Commands.Models
{
    public class CreateSupplierCommand : IRequest<Response<int>>
    {
        public SuuplierDto Dto { get; set; } = null!;
    }
}
