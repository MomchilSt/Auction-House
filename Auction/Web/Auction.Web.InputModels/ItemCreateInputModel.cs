namespace Auction.Web.InputModels
{
    public class ItemCreateInputModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public decimal StartingPrice { get; set; }

        public decimal BuyOutPrice { get; set; }

        public int AuctionDuration { get; set; }

        public string AuctionHouse { get; set; }
    }
}
