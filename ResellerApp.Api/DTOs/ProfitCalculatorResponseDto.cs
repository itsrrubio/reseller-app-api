namespace ResellerApp.Api.DTOs
{
    public class ProfitCalculatorResponseDto
    {
        public decimal MinimumSalePrice { get; set; }

        public decimal ExpectedProfit { get; set; }

        public decimal ProfitMarginPercent { get; set; }
    }
}
