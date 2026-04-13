using ApplicationLayer.Base;
using MediatR;
using System.Collections.Generic;
using ApplicationLayer.Features.Inventory.InvItemcardCategory.Dtos;

namespace ApplicationLayer.Features.Inventory.InvItemcardCategory.Queries.Models
{
    public class GetAllInvItemcardCategoriesQuery : IRequest<Response<List<InvItemcardCategoryDto>>>
    {
    }
}
