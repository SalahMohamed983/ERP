using ApplicationLayer.Features.AuthAndPermissions.Permission.Commands.Models;
using ApplicationLayer.Features.AuthAndPermissions.Permission.Queries.Models;
using ApplicationLayer.Features.AuthAndPermissions.Permission.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ERP_System.Controllers;

namespace API_ERP_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : AppControllerBase
    {
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllPermissionsQuery());
            if (result != null && result.GetType().IsGenericType && result.GetType().GetGenericTypeDefinition() == typeof(ApplicationLayer.Base.Response<>))
                return NewResult((dynamic)result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PermissionDto permissionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var command = new CreatePermissionCommand { Permission = permissionDto };
            var result = await Mediator.Send(command);

            if (result != null && result.GetType().IsGenericType && result.GetType().GetGenericTypeDefinition() == typeof(ApplicationLayer.Base.Response<>))
                return NewResult((dynamic)result);

            // handler returned raw id
            return Ok(new { id = result, message = "Permission created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PermissionDto permissionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != permissionDto.Id) return BadRequest(new { message = "Permission ID mismatch." });

            var command = new UpdatePermissionCommand { Permission = permissionDto };
            var result = await Mediator.Send(command);

            if (result != null && result.GetType().IsGenericType && result.GetType().GetGenericTypeDefinition() == typeof(ApplicationLayer.Base.Response<>))
                return NewResult((dynamic)result);

            return Ok(new { message = "Permission updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeletePermissionCommand { PermissionId = id };
            var result = await Mediator.Send(command);

            if (result != null && result.GetType().IsGenericType && result.GetType().GetGenericTypeDefinition() == typeof(ApplicationLayer.Base.Response<>))
                return NewResult((dynamic)result);

            return Ok(new { message = "Permission deleted successfully." });
        }
    }
}
