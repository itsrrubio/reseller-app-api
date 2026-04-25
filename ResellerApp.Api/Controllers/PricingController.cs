using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResellerApp.Api.DTOs;

namespace ResellerApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PricingController : ControllerBase
    {
        [HttpPost("calculate")]
        public ActionResult<ProfitCalculatorResponseDto> Calculate(
            ProfitCalculatorRequestDto dto)
        {
            decimal feeRate = dto.MarketplaceFeePercent / 100m;

            decimal minimumSalePrice =
                (dto.Cost + dto.DesiredProfit + dto.ShippingCost)
                / (1 - feeRate);

            decimal profitMargin =
                (dto.DesiredProfit / minimumSalePrice) * 100;

            return Ok(new ProfitCalculatorResponseDto
            {
                MinimumSalePrice = decimal.Round(minimumSalePrice, 2),
                ExpectedProfit = dto.DesiredProfit,
                ProfitMarginPercent = decimal.Round(profitMargin, 2)
            });
        }

    }
}
