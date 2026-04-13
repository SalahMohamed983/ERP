using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Dtos;

namespace ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Queries.Models
{
    public class GetTreasuriesDeliveryQuery : IRequest<Response<TreasuriesDeliveryDto>>
    {
        public int Id { get; set; }
    }
}
