using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ERP_System.Controllers;
using ApplicationLayer.Features.AuthAndPermissions.Role.Queries.Models;
using ApplicationLayer.Features.AuthAndPermissions.Role.Commands.Models;
using ApplicationLayer.Features.AuthAndPermissions.Role.Dtos;

namespace API_ERP_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : AppControllerBase
    {


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetAllRolesQuery());
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(ApplicationLayer.Base.Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await Mediator.Send(new CreateRoleCommand { Role = dto });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(ApplicationLayer.Base.Response<>))
                return NewResult((dynamic)response);

            return Ok(new { id = response, message = "Role created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] RoleDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != dto.Id) return BadRequest(new { message = "Role ID mismatch." });

            var response = await Mediator.Send(new ApplicationLayer.Features.AuthAndPermissions.Role.Commands.Models.UpdateRoleCommand { Role = dto });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(ApplicationLayer.Base.Response<>))
                return NewResult((dynamic)response);

            return Ok(new { message = "Role updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await Mediator.Send(new ApplicationLayer.Features.AuthAndPermissions.Role.Commands.Models.DeleteRoleCommand { RoleId = id });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(ApplicationLayer.Base.Response<>))
                return NewResult((dynamic)response);

            return Ok(new { message = "Role deleted successfully." });
        }
    }
}
