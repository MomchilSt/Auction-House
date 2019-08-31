using GlobalConstants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Auction.Data.Models
{
    public class AuctionHouse
    {
        public AuctionHouse()
        {
            this.Items = new HashSet<Item>();
            this.Reviews = new HashSet<Review>();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(ModelConstants.AuctionHouse.AuctionNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ModelConstants.AuctionHouse.AddressMaxLength)]
        public string Address { get; set; }

        [Required]
        [MaxLength(ModelConstants.AuctionHouse.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string CityId { get; set; }
        public City City { get; set; }

        public ICollection<Item> Items { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
