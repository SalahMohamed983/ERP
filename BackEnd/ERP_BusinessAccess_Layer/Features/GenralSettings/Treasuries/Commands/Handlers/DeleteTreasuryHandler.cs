using ApplicationLayer.Base;
using ApplicationLayer.Features.GenralSettings.Treasuries.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.GenralSettings.Treasuries.Commands.Handlers
{
    public class DeleteTreasuryHandler : IRequestHandler<DeleteTreasuryCommand, Response<Unit>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public DeleteTreasuryHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Unit>> Handle(DeleteTreasuryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.Treasury.GetByIdAsync(request.Id);
            if (entity == null) return _responseHandler.NotFound<Unit>("Treasury not found.");

            _uow.Treasury.Delete(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Deleted<Unit>();
        }
    }
}
