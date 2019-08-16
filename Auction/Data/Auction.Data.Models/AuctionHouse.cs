using System.Collections.Generic;

namespace Auction.Data.Models
{
    public class AuctionHouse
    {
        public AuctionHouse()
        {
            this.ItemsBeingAcutioned = new HashSet<Item>();
            this.Reviews = new HashSet<Review>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string CityId { get; set; }
        public City City { get; set; }

        public ICollection<Item> ItemsBeingAcutioned { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
