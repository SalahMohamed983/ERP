using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.InvItemcard.Queries.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Inventory_Settings;
using ApplicationLayer.Features.Inventory.InvItemcard.Dtos;

namespace ApplicationLayer.Features.Inventory.InvItemcard.Queries.Handlers
{
    public class GetAllInvItemcardsHandler : IRequestHandler<GetAllInvItemcardsQuery, Response<List<InvItemcardDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public GetAllInvItemcardsHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<List<InvItemcardDto>>> Handle(GetAllInvItemcardsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _uow.InvItemcard.GetAllAsync();
            var dtos = InvItemcardMapper.ToDtoList(entities);
            return _responseHandler.Success(dtos);
        }
    }
}
