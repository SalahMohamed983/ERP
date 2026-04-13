using ApplicationLayer.Base;
using MediatR;

namespace ApplicationLayer.Features.GenralSettings.Treasuries.Commands.Models
{
    public class DeleteTreasuryCommand : IRequest<Response<Unit>>
    {
        public int Id { get; set; }
    }
}
