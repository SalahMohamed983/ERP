using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.InvUom.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Inventory_Settings;

namespace ApplicationLayer.Features.Inventory.InvUom.Commands.Handlers
{
    public class CreateInvUomHandler : IRequestHandler<CreateInvUomCommand, Response<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public CreateInvUomHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<int>> Handle(CreateInvUomCommand request, CancellationToken cancellationToken)
        {
            var entity = InvUomMapper.ToEntity(request.Dto);
            await _uow.InvUom.AddAsync(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Created(entity.Id);
        }
    }
}
