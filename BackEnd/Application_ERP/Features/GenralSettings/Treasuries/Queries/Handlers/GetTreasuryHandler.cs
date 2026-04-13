using ApplicationLayer.Base;
using ApplicationLayer.Features.GenralSettings.Treasuries.Queries.Models;
using ApplicationLayer.RepoInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ERP_Business_Layer.Mapper.Genral_Settings.Treasuries;

namespace ApplicationLayer.Features.GenralSettings.Treasuries.Queries.Handlers
{
    public class GetTreasuryHandler : IRequestHandler<GetTreasuryQuery, Response<ApplicationLayer.Features.GenralSettings.Treasuries.Dtos.TreasuryDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ResponseHandler _responseHandler;

        public GetTreasuryHandler(IUnitOfWork uow, ResponseHandler responseHandler)
        {
            _uow = uow;
            _responseHandler = responseHandler;
        }

        public async Task<Response<ApplicationLayer.Features.GenralSettings.Treasuries.Dtos.TreasuryDto>> Handle(GetTreasuryQuery request, CancellationToken cancellationToken)
        {
            var entity = await _uow.Treasury.GetByIdAsync(request.Id);
            if (entity == null) return _responseHandler.NotFound<ApplicationLayer.Features.GenralSettings.Treasuries.Dtos.TreasuryDto>("Treasury not found.");
            var dto = TreasuryMapper.ToDto(entity);
            return _responseHandler.Success(dto);
        }
    }
}
