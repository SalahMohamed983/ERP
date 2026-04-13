using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.SuppliersCategory.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.Inventory.SuppliersCategory.Commands.Handlers
{
    public class DeleteSuppliersCategoryHandler : IRequestHandler<DeleteSuppliersCategoryCommand, Response<Unit>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public DeleteSuppliersCategoryHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Unit>> Handle(DeleteSuppliersCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.SuppliersCategory.GetByIdAsync(request.Id);
            if (entity == null) return _responseHandler.NotFound<Unit>("Suppliers category not found.");

            _uow.SuppliersCategory.Delete(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Deleted<Unit>();
        }
    }
}
