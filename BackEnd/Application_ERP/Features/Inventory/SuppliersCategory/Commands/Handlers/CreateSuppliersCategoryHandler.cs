using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.SuppliersCategory.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Inventory_Settings;

namespace ApplicationLayer.Features.Inventory.SuppliersCategory.Commands.Handlers
{
    public class CreateSuppliersCategoryHandler : IRequestHandler<CreateSuppliersCategoryCommand, Response<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public CreateSuppliersCategoryHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<int>> Handle(CreateSuppliersCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = SuppliersCategoryMapper.ToEntity(request.Dto);
            await _uow.SuppliersCategory.AddAsync(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Created(entity.Id);
        }
    }
}
