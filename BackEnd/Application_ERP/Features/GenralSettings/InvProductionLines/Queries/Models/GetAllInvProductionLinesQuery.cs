using ApplicationLayer.Base;
using MediatR;
using System.Collections.Generic;
using ApplicationLayer.Features.GenralSettings.InvProductionLines.Dtos;

namespace ApplicationLayer.Features.GenralSettings.InvProductionLines.Queries.Models
{
    public class GetAllInvProductionLinesQuery : IRequest<Response<List<SmallInvProductionLineDto>>>
    {
    }
}
