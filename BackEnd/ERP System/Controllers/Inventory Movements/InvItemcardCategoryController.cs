using ApplicationLayer.Base;
using ApplicationLayer.Features.Inventory.InvItemcardCategory.Commands.Models;
using ApplicationLayer.Features.Inventory.InvItemcardCategory.Dtos;
using ApplicationLayer.Features.Inventory.InvItemcardCategory.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API_ERP_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvItemcardCategoryController : ERP_System.Controllers.AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetAllInvItemcardCategoriesQuery());
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvItemcardCategoryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await Mediator.Send(new CreateInvItemcardCategoryCommand { Dto = dto });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }
    }
}
