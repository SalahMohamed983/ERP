using ApplicationLayer.Base;
using ApplicationLayer.Features.GenralSettings.InvProductionLines.Queries.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Genral_Settings.InvProductionLines;

namespace ApplicationLayer.Features.GenralSettings.InvProductionLines.Queries.Handlers
{
    public class GetAllInvProductionLinesHandler : IRequestHandler<GetAllInvProductionLinesQuery, Response<List<ApplicationLayer.Features.GenralSettings.InvProductionLines.Dtos.SmallInvProductionLineDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public GetAllInvProductionLinesHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<List<ApplicationLayer.Features.GenralSettings.InvProductionLines.Dtos.SmallInvProductionLineDto>>> Handle(GetAllInvProductionLinesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _uow.InvProductionLine.Query().ToListAsync();
            var dtos = InvProductionLineMapper.ToDtoList(entities);
            return _responseHandler.Success(dtos);
        }
    }
}
