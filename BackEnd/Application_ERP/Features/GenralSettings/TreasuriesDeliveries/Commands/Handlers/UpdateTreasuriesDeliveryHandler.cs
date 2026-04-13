using ApplicationLayer.Base;
using ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Genral_Settings.TreasuriesDeliveries;

namespace ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Commands.Handlers
{
    public class UpdateTreasuriesDeliveryHandler : IRequestHandler<UpdateTreasuriesDeliveryCommand, Response<Unit>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public UpdateTreasuriesDeliveryHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Unit>> Handle(UpdateTreasuriesDeliveryCommand request, CancellationToken cancellationToken)
        {
            var entity = TreasuriesDeliveryMapper.ToEntity(request.Dto);
            _uow.TreasuriesDelivery.Update(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Success(Unit.Value);
        }
    }
}
