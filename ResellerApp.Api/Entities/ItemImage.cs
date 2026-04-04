namespace ResellerApp.Api.Entities
{
    public class ItemImage
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public string ImageUrl { get; set; }

        public bool IsPrimary { get; set; }
    }
}
