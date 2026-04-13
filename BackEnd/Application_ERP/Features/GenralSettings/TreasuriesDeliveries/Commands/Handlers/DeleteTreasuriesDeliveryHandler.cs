using ApplicationLayer.Base;
using ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Commands.Handlers
{
    public class DeleteTreasuriesDeliveryHandler : IRequestHandler<DeleteTreasuriesDeliveryCommand, Response<Unit>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public DeleteTreasuriesDeliveryHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Unit>> Handle(DeleteTreasuriesDeliveryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.TreasuriesDelivery.GetByIdAsync(request.Id);
            if (entity == null) return _responseHandler.NotFound<Unit>("Treasuries delivery not found.");

            _uow.TreasuriesDelivery.Delete(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Deleted<Unit>();
        }
    }
}
