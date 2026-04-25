using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResellerApp.Api.Data;
using ResellerApp.Api.DTOs;
using ResellerApp.Api.Entities;
using ResellerApp.Api.Services;

namespace ResellerApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _service;

        public ItemsController(IItemService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateItem(CreateItemDto dto)
        {
            var id = await _service.CreateItemAsync(dto);
            return Ok(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<ItemResponseDto>>> GetItems()
        {
            return Ok(await _service.GetItemsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemResponseDto>> GetItem(int id)
        {
            var item = await _service.GetItemByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, UpdateItemDto dto)
        {
            var updated = await _service.UpdateItemAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var deleted = await _service.DeleteItemAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpPost("{id}/pricing")]
        public async Task<IActionResult> CalculateAndSavePricing(
    int id,
    ItemPricingUpdateDto dto)
        {
            var updated =
                await _service.CalculateAndSavePricingAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }
    }
}