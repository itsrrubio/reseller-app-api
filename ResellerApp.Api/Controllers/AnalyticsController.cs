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
    }
}
