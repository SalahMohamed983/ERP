using ApplicationLayer.Base;
using MediatR;
using System.Collections.Generic;
using ApplicationLayer.Features.Inventory.InvUom.Dtos;

namespace ApplicationLayer.Features.Inventory.InvUom.Queries.Models
{
    public class GetAllInvUomsQuery : IRequest<Response<List<InvUomDto>>>
    {
    }
}
