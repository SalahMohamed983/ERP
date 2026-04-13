using ApplicationLayer.Base;
using MediatR;
using System.Collections.Generic;
using ApplicationLayer.Features.Inventory.InvItemcard.Dtos;

namespace ApplicationLayer.Features.Inventory.InvItemcard.Queries.Models
{
    public class GetAllInvItemcardsQuery : IRequest<Response<List<InvItemcardDto>>>
    {
    }
}
