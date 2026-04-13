using ApplicationLayer.Base;
using ApplicationLayer.Features.GenralSettings.Treasuries.Dtos;
using MediatR;
using System.Collections.Generic;

namespace ApplicationLayer.Features.GenralSettings.Treasuries.Queries.Models
{
    public class GetAllTreasuriesQuery : IRequest<Response<List<TreasuryDto>>>
    {
    }
}

