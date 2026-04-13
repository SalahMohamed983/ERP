using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.GenralSettings.InvProductionLines.Dtos;

namespace ApplicationLayer.Features.GenralSettings.InvProductionLines.Commands.Models
{
    public class CreateInvProductionLineCommand : IRequest<Response<long>>
    {
        public InvProductionLineDto Dto { get; set; } = null!;
    }
}
