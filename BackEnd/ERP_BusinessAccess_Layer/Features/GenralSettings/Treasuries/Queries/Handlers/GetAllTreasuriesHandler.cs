using ApplicationLayer.Base;
using ApplicationLayer.Features.GenralSettings.Treasuries.Dtos;
using ApplicationLayer.Features.GenralSettings.Treasuries.Queries.Models;
using ApplicationLayer.RepoInterfaces;
using ERP_Business_Layer.Mapper.Genral_Settings.Treasuries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.GenralSettings.Treasuries.Queries.Handlers
{
    public class GetAllTreasuriesHandler : IRequestHandler<GetAllTreasuriesQuery, Response<List<TreasuryDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public GetAllTreasuriesHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<List<TreasuryDto>>> Handle(GetAllTreasuriesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _uow.Treasury.GetAllAsync();
            var dtos = TreasuryMapper.ToDtoList(entities);
            return _responseHandler.Success(dtos);
        }
    }
}

