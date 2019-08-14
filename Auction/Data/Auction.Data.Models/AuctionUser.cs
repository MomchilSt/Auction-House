using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Auction.Data.Models
{
    public class AuctionUser : IdentityUser
    {
        public AuctionUser()
        {
            this.Inventory = new HashSet<Item>();
            this.Bids = new HashSet<Bid>();
        }

        public string FullName { get; set; }

        public ICollection<Item> Inventory { get; set; }

        public ICollection<Bid> Bids { get; set; }
    }
}
