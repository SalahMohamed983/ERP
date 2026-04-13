using ApplicationLayer.Base;
using ApplicationLayer.Features.GenralSettings.InvProductionLines.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Genral_Settings.InvProductionLines;

namespace ApplicationLayer.Features.GenralSettings.InvProductionLines.Commands.Handlers
{
    public class UpdateInvProductionLineHandler : IRequestHandler<UpdateInvProductionLineCommand, Response<Unit>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public UpdateInvProductionLineHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Unit>> Handle(UpdateInvProductionLineCommand request, CancellationToken cancellationToken)
        {
            var entity = InvProductionLineMapper.ToEntity(request.Dto);
            _uow.InvProductionLine.Update(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Success(Unit.Value);
        }
    }
}
