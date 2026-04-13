using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.InvUom.Queries.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Inventory_Settings;
using ApplicationLayer.Features.Inventory.InvUom.Dtos;

namespace ApplicationLayer.Features.Inventory.InvUom.Queries.Handlers
{
    public class GetAllInvUomsHandler : IRequestHandler<GetAllInvUomsQuery, Response<List<InvUomDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public GetAllInvUomsHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<List<InvUomDto>>> Handle(GetAllInvUomsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _uow.InvUom.GetAllAsync();
            var dtos = InvUomMapper.ToDtoList(entities);
            return _responseHandler.Success(dtos);
        }
    }
}
