using ApplicationLayer.Base;
using ApplicationLayer.Features.GenralSettings.InvProductionLines.Queries.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Genral_Settings.InvProductionLines;

namespace ApplicationLayer.Features.GenralSettings.InvProductionLines.Queries.Handlers
{
    public class GetInvProductionLineHandler : IRequestHandler<GetInvProductionLineQuery, Response<ApplicationLayer.Features.GenralSettings.InvProductionLines.Dtos.InvProductionLineDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public GetInvProductionLineHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<ApplicationLayer.Features.GenralSettings.InvProductionLines.Dtos.InvProductionLineDto>> Handle(GetInvProductionLineQuery request, CancellationToken cancellationToken)
        {
            var entity = await _uow.InvProductionLine.Query().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (entity == null) return _responseHandler.NotFound<ApplicationLayer.Features.GenralSettings.InvProductionLines.Dtos.InvProductionLineDto>("Production line not found.");
            var dto = InvProductionLineMapper.ToDto(entity);
            return _responseHandler.Success(dto);
        }
    }
}
