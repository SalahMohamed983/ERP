using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.SuppliersCategory.Commands.Models;
using ApplicationLayer.Features.Inventory.SuppliersCategory.Dtos;
using ApplicationLayer.Features.Inventory.SuppliersCategory.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_ERP_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersCategoryController : ERP_System.Controllers.AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetAllSuppliersCategoriesQuery());
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SuppliersCategoryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await Mediator.Send(new CreateSuppliersCategoryCommand { Dto = dto });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }
    }
}
