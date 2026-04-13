//using ERP_Business_Layer.Interfaces.Inventory_Settings;
//using ERP_Dtos_Layer.Inventory_Settings;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using static ERP_Business_Layer.Services.Inventory_Settings.StoreService;

//namespace ERP_System.Controllers.Inventory_Movements
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class StoreController : ControllerBase
//    {
//        private readonly IStore _Store;
//        public StoreController(IStore Store)
//        {
//            _Store = Store;
//        }

//        [HttpGet("{id}")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        public async Task<ActionResult<StoreDto>> Get(int id)
//        {
//            if (id < 0)
//                return BadRequest("Parameter Are Wrong");


//            var Store = await _Store.Get(id);

//            return Store != null ? Ok(Store) : NotFound("Store Not Found");
//        }

//        // POST api/<Store>
//        [HttpPost]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<ActionResult<int>> Post(StoreDto Store)
//        {

//            Store.Id = 0;
//            _Store.StoreDto = Store;
//            if (await _Store.Save())
//            {
//                return Ok(_Store.Id);

//            }
//            else
//                return StatusCode(500, "Error!");


//        }

//        //// PUT api/<Store>/5
//        [HttpPut]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        public async Task<ActionResult<bool>> Put(StoreDto StoreDto)
//        {
//            try
//            {
//                if (StoreDto.Id < 0)
//                    return BadRequest("Parameter Are Wrong");


//                var Store = await _Store.Get(StoreDto.Id, enMode.enUpdate);

//                if (Store == null)
//                    return NotFound("Store Not Found!");

//                _Store.StoreDto = StoreDto;

//                if (await _Store.Save())
//                    return Ok("Store Updated Successfully!");

//                return StatusCode(500, "Error while updating!");
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, ex.Message);
//            }

//        }

//        //// DELETE api/<Store>/5
//        [HttpDelete("{id}")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        public async Task<ActionResult> Delete(int id)
//        {

//            if (id < 0)
//                return BadRequest("Parameter Are Wrong");


//            if (await _Store.Remove(id))
//                return Ok("Delete Successfully!");
//            else
//                return NotFound("Store Not Found!");

//        }
//    }
//}
