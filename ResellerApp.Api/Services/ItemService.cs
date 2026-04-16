using Microsoft.EntityFrameworkCore;
using ResellerApp.Api.Data;
using ResellerApp.Api.DTOs;
using ResellerApp.Api.Entities;

namespace ResellerApp.Api.Services
{
    public class ItemService : IItemService
    {
        private readonly AppDbContext _context;

        public ItemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateItemAsync(CreateItemDto dto)
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

            if (dto.ImageUrls != null && dto.ImageUrls.Any())
            {
                var images = dto.ImageUrls.Select(url => new ItemImage
                {
                    ItemId = item.Id,
                    ImageUrl = url
                });

                _context.ItemImages.AddRange(images);
                await _context.SaveChangesAsync();
            }

            return item.Id;
        }

        public async Task<List<ItemResponseDto>> GetItemsAsync()
        {
            return await _context.Items
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
        }

        public async Task<ItemResponseDto> GetItemByIdAsync(int id)
        {
            return await _context.Items
                .Include(i => i.Images)
                .Where(i => i.Id == id)
                .Select(i => new ItemResponseDto
                {
                    Id = i.Id,
                    SKU = i.SKU,
                    Description = i.Description,
                    Cost = i.Cost,
                    Images = i.Images.Select(img => img.ImageUrl).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateItemAsync(int id, UpdateItemDto dto)
        {
            var item = await _context.Items
                .Include(i => i.Images)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null) return false;

            item.SKU = dto.SKU;
            item.Description = dto.Description;
            item.Cost = dto.Cost;

            _context.ItemImages.RemoveRange(item.Images);

            if (dto.ImageUrls != null)
            {
                item.Images = dto.ImageUrls.Select(url => new ItemImage
                {
                    ImageUrl = url
                }).ToList();
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return false;

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
