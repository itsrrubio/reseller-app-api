using System.ComponentModel.DataAnnotations;

namespace ResellerApp.Api.DTOs
{
    public class CreateItemDto
    {
        [Required]
        public string SKU { get; set; }

        [Required]
        public string Description { get; set; }

        public string Category { get; set; }
        public string CountryOfOrigin { get; set; }

        [Range(0, 9999)]
        public decimal Length { get; set; }

        [Range(0, 9999)]
        public decimal Width { get; set; }

        [Range(0, 9999)]
        public decimal Height { get; set; }

        [Range(0, 9999)]
        public decimal Weight { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public string Location { get; set; }

        public string PurchasedFrom { get; set; }

        public DateTime DatePurchased { get; set; }

        [Range(0, 999999)]
        public decimal Cost { get; set; }

        [Range(0, 999999)]
        public decimal DesiredProfit { get; set; }

        public string Notes { get; set; }

        public List<string> ImageUrls { get; set; }
    }
    //public class CreateItemDto
    //{
    //    public string SKU { get; set; }
    //    public string Description { get; set; }
    //    public string Category { get; set; }
    //    public string CountryOfOrigin { get; set; }

    //    public decimal Length { get; set; }
    //    public decimal Width { get; set; }
    //    public decimal Height { get; set; }

    //    public decimal Weight { get; set; }

    //    public int Quantity { get; set; }
    //    public string Location { get; set; }

    //    public string PurchasedFrom { get; set; }
    //    public DateTime DatePurchased { get; set; }

    //    public decimal Cost { get; set; }
    //    public decimal DesiredProfit { get; set; }

    //    public string Notes { get; set; }

    //    public List<string> ImageUrls { get; set; }
    //}
}
