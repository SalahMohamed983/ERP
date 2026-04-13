using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.Stores.Queries.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Inventory_Settings;
using ApplicationLayer.Features.Inventory.Stores.Dtos;

namespace ApplicationLayer.Features.Inventory.Stores.Queries.Handlers
{
    public class GetAllStoresHandler : IRequestHandler<GetAllStoresQuery, Response<List<StoreDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public GetAllStoresHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<List<StoreDto>>> Handle(GetAllStoresQuery request, CancellationToken cancellationToken)
        {
            var entities = await _uow.Store.GetAllAsync();
            var dtos = StoreMapper.ToDtoList(entities);
            return _responseHandler.Success(dtos);
        }
    }
}
