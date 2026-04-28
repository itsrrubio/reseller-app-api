namespace ResellerApp.Api.DTOs
{
    public class SellThroughDto
    {
        public int TotalItems { get; set; }

        public int SoldItems { get; set; }

        public decimal SellThroughRatePercent { get; set; }
    }
}
