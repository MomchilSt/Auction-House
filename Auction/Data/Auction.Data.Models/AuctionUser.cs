﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Auction.Data.Models
{
    public class AuctionUser : IdentityUser
    {
        public AuctionUser()
        {
            this.ItemsAuctioned = new HashSet<Item>();
            this.Bids = new HashSet<Bid>();
            this.Receipts = new HashSet<Receipt>();
        }

        public string FullName { get; set; }

        public ICollection<Item> ItemsAuctioned { get; set; }

        public ICollection<Receipt> Receipts { get; set; }

        public ICollection<Bid> Bids { get; set; }
    }
}
