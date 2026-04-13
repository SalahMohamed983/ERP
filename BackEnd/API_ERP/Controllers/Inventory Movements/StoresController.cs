using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.Stores.Commands.Models;
using ApplicationLayer.Features.Inventory.Stores.Dtos;
using ApplicationLayer.Features.Inventory.Stores.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_ERP_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoresController : ERP_System.Controllers.AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetAllStoresQuery());
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StoreDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await Mediator.Send(new CreateStoreCommand { Dto = dto });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }
    }
}
