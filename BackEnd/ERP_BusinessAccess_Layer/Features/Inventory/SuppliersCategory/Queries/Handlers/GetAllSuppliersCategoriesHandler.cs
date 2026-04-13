using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.SuppliersCategory.Queries.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Inventory_Settings;
using ApplicationLayer.Features.Inventory.SuppliersCategory.Dtos;

namespace ApplicationLayer.Features.Inventory.SuppliersCategory.Queries.Handlers
{
    public class GetAllSuppliersCategoriesHandler : IRequestHandler<GetAllSuppliersCategoriesQuery, Response<List<SuppliersCategoryDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public GetAllSuppliersCategoriesHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<List<SuppliersCategoryDto>>> Handle(GetAllSuppliersCategoriesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _uow.SuppliersCategory.GetAllAsync();
            var dtos = SuppliersCategoryMapper.ToDtoList(entities);
            return _responseHandler.Success(dtos);
        }
    }
}
