using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.Inventory.InvUom.Dtos;

namespace ApplicationLayer.Features.Inventory.InvUom.Commands.Models
{
    public class CreateInvUomCommand : IRequest<Response<int>>
    {
        public InvUomDto Dto { get; set; } = null!;
    }
}
