namespace ResellerApp.Api.Entities
{
    public class Listing
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public string Platform { get; set; }

        public decimal Price { get; set; }

        public DateTime? DateListed { get; set; }

        public string ExternalListingId { get; set; }

        public ListingStatus Status { get; set; }
    }
}
