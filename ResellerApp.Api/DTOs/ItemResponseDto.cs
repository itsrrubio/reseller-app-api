namespace ResellerApp.Api.DTOs
{
    public class ItemResponseDto
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }

        public decimal Cost { get; set; }

        public List<string> Images { get; set; }
    }
}
