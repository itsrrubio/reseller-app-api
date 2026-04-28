namespace ResellerApp.Api.DTOs
{
    public class UnderpricedItemDto
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public decimal SuggestedListingPrice { get; set; }
        public decimal EstimatedNetProfit { get; set; }
        public decimal ProfitMarginPercent { get; set; }
        public decimal TargetMarginPercent { get; set; }
    }
}
