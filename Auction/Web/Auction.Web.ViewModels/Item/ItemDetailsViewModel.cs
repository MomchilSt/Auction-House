using System;

namespace Auction.Web.ViewModels.Item
{
    public class ItemDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal StartingPrice { get; set; }

        public decimal BuyOutPrice { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public TimeSpan RemainingTime => this.EndTime - DateTime.UtcNow;

        public string AuctionHouseId { get; set; }
        public ItemDetailsAuctionHouseViewModel AuctionHouse { get; set; }

        public string Picture { get; set; }
    }
}   
