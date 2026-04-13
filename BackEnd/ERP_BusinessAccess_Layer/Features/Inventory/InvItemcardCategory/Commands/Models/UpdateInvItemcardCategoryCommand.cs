using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.Inventory.InvItemcardCategory.Dtos;

namespace ApplicationLayer.Features.Inventory.InvItemcardCategory.Commands.Models
{
    public class UpdateInvItemcardCategoryCommand : IRequest<Response<Unit>>
    {
        public InvItemcardCategoryDto Dto { get; set; } = null!;
    }
}
