using ApplicationLayer.Base;
using MediatR;

namespace ApplicationLayer.Features.GenralSettings.InvProductionLines.Commands.Models
{
    public class DeleteInvProductionLineCommand : IRequest<Response<Unit>>
    {
        public long Id { get; set; }
    }
}
