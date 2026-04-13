using ApplicationLayer.Base;
using ApplicationLayer.Features.GenralSettings.InvProductionLines.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Genral_Settings.InvProductionLines;

namespace ApplicationLayer.Features.GenralSettings.InvProductionLines.Commands.Handlers
{
    public class CreateInvProductionLineHandler : IRequestHandler<CreateInvProductionLineCommand, Response<long>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public CreateInvProductionLineHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<long>> Handle(CreateInvProductionLineCommand request, CancellationToken cancellationToken)
        {
            var entity = InvProductionLineMapper.ToEntity(request.Dto);
            await _uow.InvProductionLine.AddAsync(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Created(entity.Id);
        }
    }
}
