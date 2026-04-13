using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.SuppliersCategory.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Inventory_Settings;

namespace ApplicationLayer.Features.Inventory.SuppliersCategory.Commands.Handlers
{
    public class UpdateSuppliersCategoryHandler : IRequestHandler<UpdateSuppliersCategoryCommand, Response<Unit>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public UpdateSuppliersCategoryHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Unit>> Handle(UpdateSuppliersCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = SuppliersCategoryMapper.ToEntity(request.Dto);
            _uow.SuppliersCategory.Update(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Success(Unit.Value);
        }
    }
}
