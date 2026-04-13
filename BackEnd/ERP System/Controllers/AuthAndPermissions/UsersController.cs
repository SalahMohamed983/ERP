using ApplicationLayer.Common;
using ApplicationLayer.Features.AuthAndPermissions.User.Commands.Models;
using ApplicationLayer.Features.AuthAndPermissions.User.Queries.Models;
using ERP_System.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ERP_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : AppControllerBase
    {
        
        [HttpPost("/roles")]
        public async Task<IActionResult> AssignRole([FromBody] ApplicationLayer.Features.AuthAndPermissions.RolePermission.Dtos.AssignRoleDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await Mediator.Send(new AssignRoleCommand { UserId = dto.UserId, RoleId = dto.RoleId });
            return NewResult(response);
        }

        [HttpDelete("{userId}/roles/{roleId}")]
        public async Task<IActionResult> RemoveRole(Guid userId, Guid roleId)
        {
            var response = await Mediator.Send(new RemoveRoleCommand { UserId = userId, RoleId = roleId });
            return NewResult(response);
        }

        [HttpGet("{userId}/roles")]
        public async Task<IActionResult> GetUserRoles(Guid userId)
        {
            var rolesResponse = await Mediator.Send(new ApplicationLayer.Features.AuthAndPermissions.Role.Queries.Models.GetAllRolesQuery());
            if (rolesResponse != null && rolesResponse.GetType().IsGenericType && rolesResponse.GetType().GetGenericTypeDefinition() == typeof(ApplicationLayer.Base.Response<>))
                return NewResult((dynamic)rolesResponse);

            var roles = (IEnumerable<ApplicationLayer.Features.AuthAndPermissions.Role.Dtos.RoleDto>)rolesResponse;

            // This endpoint previously returned role objects; return roles list
            return Ok(roles);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] PagedRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await Mediator.Send(new GetAllUsersQuery { Request = request });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(ApplicationLayer.Base.Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var user = await Mediator.Send(new GetAllUsersQuery { Request = new PagedRequestDto { PageNumber = 1, PageSize = 1 } });
            if (user != null && user.GetType().IsGenericType && user.GetType().GetGenericTypeDefinition() == typeof(ApplicationLayer.Base.Response<>))
                return NewResult((dynamic)user);

            return Ok(user);
        }

         }
}
