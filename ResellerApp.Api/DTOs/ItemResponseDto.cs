namespace ResellerApp.Api.DTOs
{
    public class ItemResponseDto
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public decimal SuggestedListingPrice { get; set; }
        public decimal EstimatedNetProfit { get; set; }
        public decimal EstimatedShippingCost { get; set; }
        public decimal EstimatedMarketplaceFeePercent { get; set; }

        public List<string> Images { get; set; }
    }
}
