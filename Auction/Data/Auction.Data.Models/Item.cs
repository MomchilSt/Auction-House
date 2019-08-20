using Auction.Data.Models.Enums;
using System;
using System.Collections.Generic;

namespace Auction.Data.Models
{
    public class Item
    {
        public Item()
        {
            this.Bids = new HashSet<Bid>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public decimal StartingPrice { get; set; }

        public decimal BuyOutPrice { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string AuctionHouseId { get; set; }
        public AuctionHouse AuctionHouse { get; set; }

        public ICollection<Bid> Bids { get; set; }
    }
}
