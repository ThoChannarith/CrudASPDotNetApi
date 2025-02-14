using CrudApi.Dtos;
using CrudApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers
{
    [Route("item/")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        public readonly ItemService itemService;

        public ItemController(ItemService itemService)
        {
            this.itemService = itemService;
        }

        [HttpPost("add_item")]
        public IActionResult AddItem([FromBody] ItemCreateDto itemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var item = itemService.AddItem(itemDto);
                return Ok(item);
            }
            catch (ArgumentException ex)
            {
                return Ok(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("get_all_item")]
        public IActionResult GetAllItem() 
        {
            try
            { 
                var item = itemService.GetAllItem();
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
