using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.InvItemcardCategory.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Inventory_Settings;

namespace ApplicationLayer.Features.Inventory.InvItemcardCategory.Commands.Handlers
{
    public class UpdateInvItemcardCategoryHandler : IRequestHandler<UpdateInvItemcardCategoryCommand, Response<Unit>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public UpdateInvItemcardCategoryHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Unit>> Handle(UpdateInvItemcardCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = InvItemcardCategoryMapper.ToEntity(request.Dto);
            _uow.InvItemcardCategory.Update(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Success(Unit.Value);
        }
    }
}
