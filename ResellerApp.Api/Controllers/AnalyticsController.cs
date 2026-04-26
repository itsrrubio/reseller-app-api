using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResellerApp.Api.Data;
using ResellerApp.Api.DTOs;

namespace ResellerApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AnalyticsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnalyticsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("summary")]
        public async Task<ActionResult<AnalyticsSummaryDto>> GetSummary()
        {
            var totalCost =
                await _context.Items.SumAsync(i =>
                    i.Cost * i.Quantity);

            var projectedProfit =
                await _context.Items.SumAsync(i =>
                    i.EstimatedNetProfit * i.Quantity);

            var itemCount =
                await _context.Items.SumAsync(i =>
                    i.Quantity);

            var projectedRevenue =
                totalCost + projectedProfit;

            return Ok(new AnalyticsSummaryDto
            {
                TotalInventoryCost = totalCost,
                ProjectedProfit = projectedProfit,
                ProjectedRevenue = projectedRevenue,
                ItemCount = itemCount
            });
        }

        [HttpGet("high-margin")]
        public async Task<ActionResult<List<HighMarginItemDto>>> GetHighMarginItems()
        {
            var items = await _context.Items
                .Where(i => i.SuggestedListingPrice > 0)
                .Select(i => new HighMarginItemDto
                {
                    Id = i.Id,
                    SKU = i.SKU,
                    Description = i.Description,
                    Cost = i.Cost,
                    SuggestedListingPrice = i.SuggestedListingPrice,
                    EstimatedNetProfit = i.EstimatedNetProfit,
                    ProfitMarginPercent =
                        Math.Round(
                            (i.EstimatedNetProfit / i.SuggestedListingPrice) * 100,
                            2)
                })
                .OrderByDescending(i => i.ProfitMarginPercent)
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("underpriced")]
        public async Task<ActionResult<List<UnderpricedItemDto>>> GetUnderpricedItems()
        {
            decimal targetMargin = 40m;

            var items = await _context.Items
                .Where(i => i.SuggestedListingPrice > 0)
                .Select(i => new UnderpricedItemDto
                {
                    Id = i.Id,
                    SKU = i.SKU,
                    Description = i.Description,
                    SuggestedListingPrice = i.SuggestedListingPrice,
                    EstimatedNetProfit = i.EstimatedNetProfit,
                    ProfitMarginPercent = Math.Round(
                        (i.EstimatedNetProfit / i.SuggestedListingPrice) * 100,
                        2),
                    TargetMarginPercent = targetMargin
                })
                .Where(i => i.ProfitMarginPercent < targetMargin)
                .OrderBy(i => i.ProfitMarginPercent)
                .ToListAsync();

            return Ok(items);
        }
    }
}
