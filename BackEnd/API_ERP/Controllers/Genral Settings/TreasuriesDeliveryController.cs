using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Dtos;
using ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Queries.Models;
using ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Commands.Models;

namespace API_ERP_Layer.Controllers.Genral_Settings
{
    [ApiController]
    [Route("api/genralsettings/treasuriesdeliveries/[controller]")]
    public class TreasuriesDeliveryController : ERP_System.Controllers.AppControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 0) return BadRequest("Parameter Are Wrong");

            var response = await Mediator.Send(new GetTreasuriesDeliveryQuery { Id = id });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TreasuriesDeliveryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await Mediator.Send(new CreateTreasuriesDeliveryCommand { Dto = dto });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TreasuriesDeliveryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await Mediator.Send(new UpdateTreasuriesDeliveryCommand { Dto = dto });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0) return BadRequest("Parameter Are Wrong");

            var response = await Mediator.Send(new DeleteTreasuriesDeliveryCommand { Id = id });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }
    }
}
