using ApplicationLayer.Base;
using ApplicationLayer.Features.GenralSettings.Treasuries.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.GenralSettings.Treasuries.Commands.Handlers
{
    public class DeleteTreasuriesHandler : IRequestHandler<DeleteTreasuriesCommand, Response<Unit>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public DeleteTreasuriesHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Unit>> Handle(DeleteTreasuriesCommand request, CancellationToken cancellationToken)
        {
            if (request.Ids == null) return _responseHandler.BadRequest<Unit>("No ids provided.");
            foreach (var id in request.Ids.Distinct())
            {
                var entity = await _uow.Treasury.GetByIdAsync(id);
                if (entity != null)
                {
                    _uow.Treasury.Delete(entity);
                }
            }
            await _uow.CompleteAsync();
            return _responseHandler.Deleted<Unit>();
        }
    }
}
