using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.GenralSettings.InvProductionLines.Dtos;
using ApplicationLayer.Features.GenralSettings.InvProductionLines.Queries.Models;
using ApplicationLayer.Features.GenralSettings.InvProductionLines.Commands.Models;

namespace API_ERP_Layer.Controllers.Genral_Settings.InvProductionLines
{
    [ApiController]
    [Route("api/genralsettings/invproductionlines/[controller]")]
    public class InvProductionLineController : ERP_System.Controllers.AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetAllInvProductionLinesQuery());
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            if (id < 0) return BadRequest("Parameter Are Wrong");

            var response = await Mediator.Send(new GetInvProductionLineQuery { Id = id });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvProductionLineDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await Mediator.Send(new CreateInvProductionLineCommand { Dto = dto });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] InvProductionLineDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await Mediator.Send(new UpdateInvProductionLineCommand { Dto = dto });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (id < 0) return BadRequest("Parameter Are Wrong");

            var response = await Mediator.Send(new DeleteInvProductionLineCommand { Id = id });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }
    }
}
