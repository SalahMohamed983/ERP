using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.InvItemcardCategory.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Inventory_Settings;
using DominLayer.Entites;

namespace ApplicationLayer.Features.Inventory.InvItemcardCategory.Commands.Handlers
{
    public class CreateInvItemcardCategoryHandler : IRequestHandler<CreateInvItemcardCategoryCommand, Response<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public CreateInvItemcardCategoryHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<int>> Handle(CreateInvItemcardCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = InvItemcardCategoryMapper.ToEntity(request.Dto);
            await _uow.InvItemcardCategory.AddAsync(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Created(entity.Id);
        }
    }
}
