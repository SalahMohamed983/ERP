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
    public class CreateTreasuryHandler : IRequestHandler<CreateTreasuryCommand, Response<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public CreateTreasuryHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<int>> Handle(CreateTreasuryCommand request, CancellationToken cancellationToken)
        {
            if (request.Dto.IsMaster)
            {
                var all = await _uow.Treasury.GetAllAsync();
                foreach (var t in all.Where(x => x.IsMaster))
                {
                    t.IsMaster = false;
                    _uow.Treasury.Update(t);
                }
            }
            var entity = TreasuryMapper.ToEntity(request.Dto);
            entity.CreatedAt = DateTime.UtcNow;
            await _uow.Treasury.AddAsync(entity);
            await _uow.CompleteAsync();
            return _responseHandler.Created(entity.Id);
        }
    }
}
