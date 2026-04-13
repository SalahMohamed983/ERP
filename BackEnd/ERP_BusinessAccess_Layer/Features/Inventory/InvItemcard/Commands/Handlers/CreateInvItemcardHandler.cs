using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.InvItemcard.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Inventory_Settings;

namespace ApplicationLayer.Features.Inventory.InvItemcard.Commands.Handlers
{
    public class CreateInvItemcardHandler : IRequestHandler<CreateInvItemcardCommand, Response<long>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public CreateInvItemcardHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<long>> Handle(CreateInvItemcardCommand request, CancellationToken cancellationToken)
        {
            var entity = InvItemcardMapper.ToEntity(request.Dto);
            await _uow.InvItemcard.AddAsync(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Created(entity.Id);
        }
    }
}
