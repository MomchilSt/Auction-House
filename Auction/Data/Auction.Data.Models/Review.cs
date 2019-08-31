using GlobalConstants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Auction.Data.Models
{
    public class Review
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(ModelConstants.Review.AuthorNameMaxLength)]
        public string Author { get; set; }

        [Required]
        [MaxLength(ModelConstants.Review.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string AuctionHouseId { get; set; }
        public AuctionHouse AuctionHouse { get; set; }
    }
}
