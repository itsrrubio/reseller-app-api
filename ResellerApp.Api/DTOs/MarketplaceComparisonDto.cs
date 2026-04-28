namespace ResellerApp.Api.DTOs
{
    public class MarketplaceComparisonDto
    {
        public string Platform { get; set; }
        public decimal FeePercent { get; set; }
        public decimal EstimatedNetProfit { get; set; }
    }
}
