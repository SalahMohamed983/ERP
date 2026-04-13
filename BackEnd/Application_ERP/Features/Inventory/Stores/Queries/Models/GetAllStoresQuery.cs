using ApplicationLayer.Base;
using MediatR;
using System.Collections.Generic;
using ApplicationLayer.Features.Inventory.Stores.Dtos;

namespace ApplicationLayer.Features.Inventory.Stores.Queries.Models
{
    public class GetAllStoresQuery : IRequest<Response<List<StoreDto>>>
    {
    }
}
