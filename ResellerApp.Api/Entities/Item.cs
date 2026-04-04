using System.Reflection;

namespace ResellerApp.Api.Entities
{
    public class Item
    {
        public int Id { get; set; }

        public string SKU { get; set; }

        public string Description { get; set; }
        public string Category { get; set; }
        public string CountryOfOrigin { get; set; }

        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }

        public decimal Weight { get; set; }

        public int Quantity { get; set; }
        public string Location { get; set; }

        public string PurchasedFrom { get; set; }
        public DateTime DatePurchased { get; set; }

        public decimal Cost { get; set; }
        public decimal DesiredProfit { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Listing> Listings { get; set; }
        public ICollection<ItemImage> Images { get; set; }
    }
}
