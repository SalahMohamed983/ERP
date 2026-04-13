using ApplicationLayer.Base;
using MediatR;

namespace ApplicationLayer.Features.Inventory.SuppliersCategory.Commands.Models
{
    public class DeleteSuppliersCategoryCommand : IRequest<Response<Unit>>
    {
        public int Id { get; set; }
    }
}
