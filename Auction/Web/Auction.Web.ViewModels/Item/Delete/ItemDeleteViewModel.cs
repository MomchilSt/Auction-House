namespace Auction.Web.ViewModels.Item.Delete
{
    public class ItemDeleteViewModel
    {
        public string Name { get; set; }

        public ItemDeleteCategoryViewModel Category { get; set; }

        public decimal StartingPrice { get; set; }

        public decimal BuyOutPrice { get; set; }

        public string Picture { get; set; }

        public string AuctionDuration { get; set; }

        public string AuctionHouse { get; set; }

        public string Description { get; set; }
    }
}
