using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.Inventory.InvItemcardCategory.Dtos;

namespace ApplicationLayer.Features.Inventory.InvItemcardCategory.Commands.Models
{
    public class CreateInvItemcardCategoryCommand : IRequest<Response<int>>
    {
        public InvItemcardCategoryDto Dto { get; set; } = null!;
    }
}
