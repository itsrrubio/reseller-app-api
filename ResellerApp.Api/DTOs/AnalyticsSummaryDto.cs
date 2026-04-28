namespace ResellerApp.Api.DTOs
{
    public class AnalyticsSummaryDto
    {
        public decimal TotalInventoryCost { get; set; }

        public decimal ProjectedProfit { get; set; }

        public decimal ProjectedRevenue { get; set; }

        public int ItemCount { get; set; }
    }
}
