using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.GenralSettings.Treasuries.Dtos;

namespace ApplicationLayer.Features.GenralSettings.Treasuries.Commands.Models
{
    public class UpdateTreasuryCommand : IRequest<Response<Unit>>
    {
        public TreasuryDto Dto { get; set; } = null!;
    }
}
