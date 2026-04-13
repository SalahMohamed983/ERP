using ApplicationLayer.Base;
using ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Genral_Settings.TreasuriesDeliveries;

namespace ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Commands.Handlers
{
    public class CreateTreasuriesDeliveryHandler : IRequestHandler<CreateTreasuriesDeliveryCommand, Response<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public CreateTreasuriesDeliveryHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<int>> Handle(CreateTreasuriesDeliveryCommand request, CancellationToken cancellationToken)
        {
            var entity = TreasuriesDeliveryMapper.ToEntity(request.Dto);
            await _uow.TreasuriesDelivery.AddAsync(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Created(entity.Id);
        }
    }
}
