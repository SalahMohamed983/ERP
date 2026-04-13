using ApplicationLayer.Base;
using ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Queries.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Genral_Settings.TreasuriesDeliveries;

namespace ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Queries.Handlers
{
    public class GetTreasuriesDeliveryHandler : IRequestHandler<GetTreasuriesDeliveryQuery, Response<ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Dtos.TreasuriesDeliveryDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public GetTreasuriesDeliveryHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Dtos.TreasuriesDeliveryDto>> Handle(GetTreasuriesDeliveryQuery request, CancellationToken cancellationToken)
        {
            var entity = await _uow.TreasuriesDelivery.GetByIdAsync(request.Id);
            if (entity == null) return _responseHandler.NotFound<ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Dtos.TreasuriesDeliveryDto>("Treasuries delivery not found.");
            var dto = TreasuriesDeliveryMapper.ToDto(entity);
            return _responseHandler.Success(dto);
        }
    }
}
