using System;

namespace Auction.Data.Models
{
    public class Bid
    {
        public string Id { get; set; }

        public decimal Amount { get; set; }

        public string ItemId { get; set; }
        public Item Item { get; set; }

        public string BuyerId { get; set; }
        public AuctionUser Buyer { get; set; }

        public DateTime MadeOn { get; set; }
    }
}
