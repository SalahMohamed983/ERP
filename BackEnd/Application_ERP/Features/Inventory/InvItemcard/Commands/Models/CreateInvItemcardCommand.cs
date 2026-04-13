using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.Inventory.InvItemcard.Dtos;

namespace ApplicationLayer.Features.Inventory.InvItemcard.Commands.Models
{
    public class CreateInvItemcardCommand : IRequest<Response<long>>
    {
        public InvItemcardDto Dto { get; set; } = null!;
    }
}
