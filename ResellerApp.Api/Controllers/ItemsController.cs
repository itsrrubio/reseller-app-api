using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResellerApp.Api.Data;
using ResellerApp.Api.DTOs;
using ResellerApp.Api.Entities;

namespace ResellerApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        // 🔥 CREATE ITEM WITH IMAGES
        [HttpPost]
        public async Task<IActionResult> CreateItem(CreateItemDto dto)
        {
            var item = new Item
            {
                SKU = dto.SKU,
                Description = dto.Description,
                Category = dto.Category,
                CountryOfOrigin = dto.CountryOfOrigin,
                Length = dto.Length,
                Width = dto.Width,
                Height = dto.Height,
                Weight = dto.Weight,
                Quantity = dto.Quantity,
                Location = dto.Location,
                PurchasedFrom = dto.PurchasedFrom,
                DatePurchased = dto.DatePurchased,
                Cost = dto.Cost,
                DesiredProfit = dto.DesiredProfit,
                Notes = dto.Notes
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            // 🔥 Add images
            if (dto.ImageUrls != null && dto.ImageUrls.Any())
            {
                var images = dto.ImageUrls.Select(url => new ItemImage
                {
                    ItemId = item.Id,
                    ImageUrl = url,
                    IsPrimary = false
                });

                _context.ItemImages.AddRange(images);
                await _context.SaveChangesAsync();
            }

            return Ok(item.Id);
        }

        // 🔥 GET ALL ITEMS WITH IMAGES
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = await _context.Items
                .Include(i => i.Images)
                .Select(i => new ItemResponseDto
                {
                    Id = i.Id,
                    SKU = i.SKU,
                    Description = i.Description,
                    Cost = i.Cost,
                    Images = i.Images.Select(img => img.ImageUrl).ToList()
                })
                .ToListAsync();

            return Ok(items);
        }
    }
}