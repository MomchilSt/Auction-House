using System.Collections.Generic;

namespace Auction.Data.Models
{
    public class Review
    {
        public string Id { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string AuctionHouseId { get; set; }
        public AuctionHouse AuctionHouse { get; set; }
    }
}
