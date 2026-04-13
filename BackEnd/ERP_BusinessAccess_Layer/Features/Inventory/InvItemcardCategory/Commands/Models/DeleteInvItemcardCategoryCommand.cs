using ApplicationLayer.Base;
using MediatR;

namespace ApplicationLayer.Features.Inventory.InvItemcardCategory.Commands.Models
{
    public class DeleteInvItemcardCategoryCommand : IRequest<Response<Unit>>
    {
        public int Id { get; set; }
    }
}
