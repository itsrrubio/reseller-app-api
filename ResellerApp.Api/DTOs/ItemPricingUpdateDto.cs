namespace ResellerApp.Api.DTOs
{
    public class ItemPricingUpdateDto
    {
        public decimal MarketplaceFeePercent { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
