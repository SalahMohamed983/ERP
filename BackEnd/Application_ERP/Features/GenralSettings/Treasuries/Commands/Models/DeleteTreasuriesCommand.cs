using ApplicationLayer.Base;
using MediatR;
using System.Collections.Generic;

namespace ApplicationLayer.Features.GenralSettings.Treasuries.Commands.Models
{
    public class DeleteTreasuriesCommand : IRequest<Response<Unit>>
    {
        public IEnumerable<int> Ids { get; set; } = null!;
    }
}
