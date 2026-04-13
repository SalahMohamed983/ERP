using ApplicationLayer.Base;
using MediatR;
using System.Collections.Generic;
using ApplicationLayer.Features.Inventory.Suppliers.Dtos;

namespace ApplicationLayer.Features.Inventory.Suppliers.Queries.Models
{
    public class GetAllSuppliersQuery : IRequest<Response<List<SuuplierDto>>>
    {
    }
}
