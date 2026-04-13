using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.InvItemcardCategory.Queries.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Inventory_Settings;
using ApplicationLayer.Features.Inventory.InvItemcardCategory.Dtos;

namespace ApplicationLayer.Features.Inventory.InvItemcardCategory.Queries.Handlers
{
    public class GetAllInvItemcardCategoriesHandler : IRequestHandler<GetAllInvItemcardCategoriesQuery, Response<List<InvItemcardCategoryDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public GetAllInvItemcardCategoriesHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<List<InvItemcardCategoryDto>>> Handle(GetAllInvItemcardCategoriesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _uow.InvItemcardCategory.GetAllAsync();
            var dtos = InvItemcardCategoryMapper.ToDtoList(entities);
            return _responseHandler.Success(dtos);
        }
    }
}
