using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResellerApp.Api.Data;
using ResellerApp.Api.DTOs;
using ResellerApp.Api.Entities;

namespace ResellerApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ListingDraftsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ListingDraftsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("from-item/{itemId}")]
        public async Task<IActionResult> CreateDraft(
            int itemId,
            CreateListingDraftDto dto)
        {
            var item = await _context.Items.FindAsync(itemId);

            if (item == null)
                return NotFound();

            var draft = new ListingDraft
            {
                ItemId = itemId,
                Marketplace = dto.Marketplace,
                Title = dto.Title,
                Description = dto.Description,
                DraftPrice = item.SuggestedListingPrice
            };

            _context.ListingDrafts.Add(draft);

            await _context.SaveChangesAsync();

            return Ok(draft.Id);
        }
    }
}
