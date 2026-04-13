using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.Inventory.Stores.Dtos;

namespace ApplicationLayer.Features.Inventory.Stores.Commands.Models
{
    public class CreateStoreCommand : IRequest<Response<int>>
    {
        public StoreDto Dto { get; set; } = null!;
    }
}
