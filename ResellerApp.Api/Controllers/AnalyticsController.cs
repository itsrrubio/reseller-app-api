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

        [HttpGet("marketplace-comparison/{itemId}")]
        public async Task<ActionResult<List<MarketplaceComparisonDto>>> CompareMarketplaces(int itemId)
        {
            var item = await _context.Items.FindAsync(itemId);

            if (item == null)
                return NotFound();

            decimal salePrice = item.SuggestedListingPrice;
            decimal shipping = item.EstimatedShippingCost;
            decimal cost = item.Cost;

            var results = new List<MarketplaceComparisonDto>
    {
        CalculatePlatform("eBay", 13m, salePrice, shipping, cost),
        CalculatePlatform("Mercari", 10m, salePrice, shipping, cost),
        CalculatePlatform("Poshmark", 20m, salePrice, shipping, cost)
    }
            .OrderByDescending(x => x.EstimatedNetProfit)
            .ToList();

            return Ok(results);
        }

        private MarketplaceComparisonDto CalculatePlatform(
            string platform,
            decimal feePercent,
            decimal salePrice,
            decimal shipping,
            decimal cost)
        {
            decimal fees = salePrice * (feePercent / 100m);

            decimal netProfit = salePrice - fees - shipping - cost;

            return new MarketplaceComparisonDto
            {
                Platform = platform,
                FeePercent = feePercent,
                EstimatedNetProfit = decimal.Round(netProfit, 2)
            };
        }
    }
}
