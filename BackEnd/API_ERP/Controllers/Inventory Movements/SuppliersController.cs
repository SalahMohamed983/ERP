using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.Suppliers.Commands.Models;
using ApplicationLayer.Features.Inventory.Suppliers.Dtos;
using ApplicationLayer.Features.Inventory.Suppliers.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_ERP_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ERP_System.Controllers.AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetAllSuppliersQuery());
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SuuplierDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await Mediator.Send(new CreateSupplierCommand { Dto = dto });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }
    }
}
