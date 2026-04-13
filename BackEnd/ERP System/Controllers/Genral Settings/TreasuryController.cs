using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.GenralSettings.Treasuries.Dtos;
using ApplicationLayer.Features.GenralSettings.Treasuries.Queries.Models;
using ApplicationLayer.Features.GenralSettings.Treasuries.Commands.Models;

namespace API_ERP_Layer.Controllers.Genral_Settings
{
    [ApiController]
    [Route("api/genralsettings/treasuries/[controller]")]
    public class TreasuryController : ERP_System.Controllers.AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetAllTreasuriesQuery());
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 0) return BadRequest("Parameter Are Wrong");

            var response = await Mediator.Send(new GetTreasuryQuery { Id = id });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TreasuryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            dto.CreatedAt = DateTime.UtcNow;
            dto.Date = DateOnly.FromDateTime(DateTime.UtcNow);

            var response = await Mediator.Send(new CreateTreasuryCommand { Dto = dto });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TreasuryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            dto.UpdatedAt = DateTime.UtcNow;
            var response = await Mediator.Send(new UpdateTreasuryCommand { Dto = dto });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Parameter are wrong.");
            var response = await Mediator.Send(new DeleteTreasuryCommand { Id = id });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);
            return Ok(response);
        }

        [HttpPost("delete-multiple")]
        public async Task<IActionResult> DeleteMultiple([FromBody] int[] ids)
        {
            if (ids == null || ids.Length == 0) return BadRequest("No ids provided.");
            var response = await Mediator.Send(new DeleteTreasuriesCommand { Ids = ids });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);
            return Ok(response);
        }
    }
}
