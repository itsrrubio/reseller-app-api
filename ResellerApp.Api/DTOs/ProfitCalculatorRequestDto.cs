using System.ComponentModel.DataAnnotations;

namespace ResellerApp.Api.DTOs
{
    public class ProfitCalculatorRequestDto
    {
    [Range(0, 999999)]
    public decimal Cost { get; set; }

    [Range(0, 999999)]
    public decimal DesiredProfit { get; set; }

    [Range(0, 100)]
    public decimal MarketplaceFeePercent { get; set; }

    [Range(0, 999999)]
    public decimal ShippingCost { get; set; }
    }
}
