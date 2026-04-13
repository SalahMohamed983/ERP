using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.Stores.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Inventory_Settings;

namespace ApplicationLayer.Features.Inventory.Stores.Commands.Handlers
{
    public class CreateStoreHandler : IRequestHandler<CreateStoreCommand, Response<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _response_handler;

        public CreateStoreHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _response_handler = responseHandler;
        }

        public async Task<Response<int>> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            var entity = StoreMapper.ToEntity(request.Dto);
            await _uow.Store.AddAsync(entity);
            await _uow.CompleteAsync();
            return _response_handler.Created(entity.Id);
        }
    }
}
