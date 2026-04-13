using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.GenralSettings.InvProductionLines.Dtos;

namespace ApplicationLayer.Features.GenralSettings.InvProductionLines.Queries.Models
{
    public class GetInvProductionLineQuery : IRequest<Response<InvProductionLineDto>>
    {
        public long Id { get; set; }
    }
}
