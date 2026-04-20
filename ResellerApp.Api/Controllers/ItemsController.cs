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
    }

    //[ApiController]
    //[Route("api/[controller]")]
    //public class ItemsController : ControllerBase
    //{
    //    private readonly AppDbContext _context;

    //    public ItemsController(AppDbContext context)
    //    {
    //        _context = context;
    //    }

    //    // CREATE
    //    [HttpPost]
    //    public async Task<ActionResult<int>> CreateItem(CreateItemDto dto)
    //    {
    //        var item = new Item
    //        {
    //            SKU = dto.SKU,
    //            Description = dto.Description,
    //            Category = dto.Category,
    //            CountryOfOrigin = dto.CountryOfOrigin,
    //            Length = dto.Length,
    //            Width = dto.Width,
    //            Height = dto.Height,
    //            Weight = dto.Weight,
    //            Quantity = dto.Quantity,
    //            Location = dto.Location,
    //            PurchasedFrom = dto.PurchasedFrom,
    //            DatePurchased = dto.DatePurchased,
    //            Cost = dto.Cost,
    //            DesiredProfit = dto.DesiredProfit,
    //            Notes = dto.Notes
    //        };

    //        _context.Items.Add(item);
    //        await _context.SaveChangesAsync();

    //        if (dto.ImageUrls != null && dto.ImageUrls.Any())
    //        {
    //            var images = dto.ImageUrls.Select(url => new ItemImage
    //            {
    //                ItemId = item.Id,
    //                ImageUrl = url
    //            });

    //            _context.ItemImages.AddRange(images);
    //            await _context.SaveChangesAsync();
    //        }

    //        return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item.Id);
    //    }

    //    // GET ALL
    //    [HttpGet]
    //    public async Task<ActionResult<List<ItemResponseDto>>> GetItems()
    //    {
    //        var items = await _context.Items
    //            .Include(i => i.Images)
    //            .Select(i => new ItemResponseDto
    //            {
    //                Id = i.Id,
    //                SKU = i.SKU,
    //                Description = i.Description,
    //                Cost = i.Cost,
    //                Images = i.Images.Select(img => img.ImageUrl).ToList()
    //            })
    //            .ToListAsync();

    //        return Ok(items);
    //    }

    //    // GET BY ID
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<ItemResponseDto>> GetItemById(int id)
    //    {
    //        var item = await _context.Items
    //            .Include(i => i.Images)
    //            .Where(i => i.Id == id)
    //            .Select(i => new ItemResponseDto
    //            {
    //                Id = i.Id,
    //                SKU = i.SKU,
    //                Description = i.Description,
    //                Cost = i.Cost,
    //                Images = i.Images.Select(img => img.ImageUrl).ToList()
    //            })
    //            .FirstOrDefaultAsync();

    //        if (item == null)
    //            return NotFound();

    //        return Ok(item);
    //    }

    //    // UPDATE
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> UpdateItem(int id, UpdateItemDto dto)
    //    {
    //        var item = await _context.Items
    //            .Include(i => i.Images)
    //            .FirstOrDefaultAsync(i => i.Id == id);

    //        if (item == null)
    //            return NotFound();

    //        // Update fields
    //        item.SKU = dto.SKU;
    //        item.Description = dto.Description;
    //        item.Category = dto.Category;
    //        item.CountryOfOrigin = dto.CountryOfOrigin;
    //        item.Length = dto.Length;
    //        item.Width = dto.Width;
    //        item.Height = dto.Height;
    //        item.Weight = dto.Weight;
    //        item.Quantity = dto.Quantity;
    //        item.Location = dto.Location;
    //        item.PurchasedFrom = dto.PurchasedFrom;
    //        item.DatePurchased = dto.DatePurchased;
    //        item.Cost = dto.Cost;
    //        item.DesiredProfit = dto.DesiredProfit;
    //        item.Notes = dto.Notes;

    //        // Replace images (simple approach)
    //        _context.ItemImages.RemoveRange(item.Images);

    //        if (dto.ImageUrls != null && dto.ImageUrls.Any())
    //        {
    //            item.Images = dto.ImageUrls.Select(url => new ItemImage
    //            {
    //                ImageUrl = url
    //            }).ToList();
    //        }

    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }

    //    // DELETE
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteItem(int id)
    //    {
    //        var item = await _context.Items.FindAsync(id);

    //        if (item == null)
    //            return NotFound();

    //        _context.Items.Remove(item);
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }
    //}


    //[ApiController]
    //[Route("api/[controller]")]
    //public class ItemsController : ControllerBase
    //{
    //    private readonly AppDbContext _context;

    //    public ItemsController(AppDbContext context)
    //    {
    //        _context = context;
    //    }

    //    // 🔥 CREATE ITEM WITH IMAGES
    //    [HttpPost]
    //    public async Task<IActionResult> CreateItem(CreateItemDto dto)
    //    {
    //        var item = new Item
    //        {
    //            SKU = dto.SKU,
    //            Description = dto.Description,
    //            Category = dto.Category,
    //            CountryOfOrigin = dto.CountryOfOrigin,
    //            Length = dto.Length,
    //            Width = dto.Width,
    //            Height = dto.Height,
    //            Weight = dto.Weight,
    //            Quantity = dto.Quantity,
    //            Location = dto.Location,
    //            PurchasedFrom = dto.PurchasedFrom,
    //            DatePurchased = dto.DatePurchased,
    //            Cost = dto.Cost,
    //            DesiredProfit = dto.DesiredProfit,
    //            Notes = dto.Notes
    //        };

    //        _context.Items.Add(item);
    //        await _context.SaveChangesAsync();

    //        // 🔥 Add images
    //        if (dto.ImageUrls != null && dto.ImageUrls.Any())
    //        {
    //            var images = dto.ImageUrls.Select(url => new ItemImage
    //            {
    //                ItemId = item.Id,
    //                ImageUrl = url,
    //                IsPrimary = false
    //            });

    //            _context.ItemImages.AddRange(images);
    //            await _context.SaveChangesAsync();
    //        }

    //        return Ok(item.Id);
    //    }

    //    // 🔥 GET ALL ITEMS WITH IMAGES
    //    [HttpGet]
    //    public async Task<IActionResult> GetItems()
    //    {
    //        var items = await _context.Items
    //            .Include(i => i.Images)
    //            .Select(i => new ItemResponseDto
    //            {
    //                Id = i.Id,
    //                SKU = i.SKU,
    //                Description = i.Description,
    //                Cost = i.Cost,
    //                Images = i.Images.Select(img => img.ImageUrl).ToList()
    //            })
    //            .ToListAsync();

    //        return Ok(items);
    //    }
    //}
}