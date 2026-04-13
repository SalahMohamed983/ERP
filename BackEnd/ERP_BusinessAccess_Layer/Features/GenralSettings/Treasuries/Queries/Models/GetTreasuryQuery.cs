using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.GenralSettings.Treasuries.Dtos;

namespace ApplicationLayer.Features.GenralSettings.Treasuries.Queries.Models
{
    public class GetTreasuryQuery : IRequest<Response<TreasuryDto>>
    {
        public int Id { get; set; }
    }
}
