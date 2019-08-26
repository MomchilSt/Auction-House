﻿using System;

namespace Auction.Data.Models
{
    public class Bid
    {
        public string Id { get; set; }

        public decimal Amount { get; set; }

        public string ItemId { get; set; }
        public Item Item { get; set; }

        public string UserId { get; set; }
        public AuctionUser User { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
