using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.Suppliers.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Inventory_Settings;

namespace ApplicationLayer.Features.Inventory.Suppliers.Commands.Handlers
{
    public class CreateSupplierHandler : IRequestHandler<CreateSupplierCommand, Response<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _response_handler;

        public CreateSupplierHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _response_handler = responseHandler;
        }

        public async Task<Response<int>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var entity = SuuplierMapper.ToEntity(request.Dto);
            await _uow.Suuplier.AddAsync(entity);
            await _uow.CompleteAsync();
            return _response_handler.Created((int)entity.Id);
        }
    }
}
