using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.GenralSettings.Treasuries.Dtos;

namespace ApplicationLayer.Features.GenralSettings.Treasuries.Commands.Models
{
    public class CreateTreasuryCommand : IRequest<Response<int>>
    {
        public TreasuryDto Dto { get; set; } = null!;
    }
}
