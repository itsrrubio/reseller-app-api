namespace ResellerApp.Api.Entities
{
    public class ListingDraft
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public string Marketplace { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal DraftPrice { get; set; }

        public string Status { get; set; } = "Draft";

        public Item Item { get; set; }
    }
}
