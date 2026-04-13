using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Dtos;

namespace ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Commands.Models
{
    public class UpdateTreasuriesDeliveryCommand : IRequest<Response<Unit>>
    {
        public TreasuriesDeliveryDto Dto { get; set; } = null!;
    }
}
