using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.Suppliers.Queries.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Inventory_Settings;
using ApplicationLayer.Features.Inventory.Suppliers.Dtos;

namespace ApplicationLayer.Features.Inventory.Suppliers.Queries.Handlers
{
    public class GetAllSuppliersHandler : IRequestHandler<GetAllSuppliersQuery, Response<List<SuuplierDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _response_handler;

        public GetAllSuppliersHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _response_handler = responseHandler;
        }

        public async Task<Response<List<SuuplierDto>>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            var entities = await _uow.Suuplier.GetAllAsync();
            var dtos = SuuplierMapper.ToDtoList(entities);
            return _response_handler.Success(dtos);
        }
    }
}
