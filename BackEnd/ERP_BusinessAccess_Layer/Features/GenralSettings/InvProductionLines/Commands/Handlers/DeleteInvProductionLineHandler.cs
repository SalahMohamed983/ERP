using ApplicationLayer.Base;
using ApplicationLayer.Features.GenralSettings.InvProductionLines.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.GenralSettings.InvProductionLines.Commands.Handlers
{
    public class DeleteInvProductionLineHandler : IRequestHandler<DeleteInvProductionLineCommand, Response<Unit>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public DeleteInvProductionLineHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Unit>> Handle(DeleteInvProductionLineCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.InvProductionLine.Query().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (entity == null) return _responseHandler.NotFound<Unit>("Production line not found.");

            _uow.InvProductionLine.Delete(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Deleted<Unit>();
        }
    }
}
