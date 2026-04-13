using ApplicationLayer.Base;
using ApplicationLayer.Features.GenralSettings.Treasuries.Commands.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Genral_Settings.Treasuries;

namespace ApplicationLayer.Features.GenralSettings.Treasuries.Commands.Handlers
{
    public class UpdateTreasuryHandler : IRequestHandler<UpdateTreasuryCommand, Response<Unit>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public UpdateTreasuryHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<Unit>> Handle(UpdateTreasuryCommand request, CancellationToken cancellationToken)
        {
            if (request.Dto.IsMaster)
            {
                var all = await _uow.Treasury.GetAllAsync();
                foreach (var t in all.Where(x => x.IsMaster && x.Id != request.Dto.Id))
                {
                    t.IsMaster = false;
                    _uow.Treasury.Update(t);
                }
            }
            var entity = TreasuryMapper.ToEntity(request.Dto);
            entity.UpdatedAt = DateTime.UtcNow;
            _uow.Treasury.Update(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Success(Unit.Value);
        }
    }
}
