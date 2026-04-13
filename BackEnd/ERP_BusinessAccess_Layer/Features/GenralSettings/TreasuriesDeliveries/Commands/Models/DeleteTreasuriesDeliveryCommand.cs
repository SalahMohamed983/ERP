using ApplicationLayer.Base;
using MediatR;

namespace ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Commands.Models
{
    public class DeleteTreasuriesDeliveryCommand : IRequest<Response<Unit>>
    {
        public int Id { get; set; }
    }
}
