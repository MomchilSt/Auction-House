using System;
using System.Collections;

namespace Auction.Data.Models
{
    public class Receipt
    {
        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public string ItemId { get; set; }

        public Item Item { get; set; }

        public string UserId { get; set; }

        public AuctionUser User { get; set; }
    }
}
