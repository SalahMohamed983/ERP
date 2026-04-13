using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.InvItemcardCategory.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.Inventory.InvItemcardCategory.Commands.Handlers
{
    public class DeleteInvItemcardCategoryHandler : IRequestHandler<DeleteInvItemcardCategoryCommand, Response<Unit>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public DeleteInvItemcardCategoryHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Unit>> Handle(DeleteInvItemcardCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.InvItemcardCategory.GetByIdAsync(request.Id);
            if (entity == null) return _responseHandler.NotFound<Unit>("Category not found.");

            _uow.InvItemcardCategory.Delete(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Deleted<Unit>();
        }
    }
}
