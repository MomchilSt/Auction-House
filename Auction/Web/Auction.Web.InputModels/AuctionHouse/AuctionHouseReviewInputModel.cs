using GlobalConstants;
using System.ComponentModel.DataAnnotations;

namespace Auction.Web.InputModels.AuctionHouse
{
    public class AuctionHouseReviewInputModel
    {
        [Required]
        [MaxLength(ModelConstants.Review.AuthorNameMaxLength)]
        public string Author { get; set; }

        [Required]
        [MaxLength(ModelConstants.Review.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string AuctionHouseName { get; set; }
    }
}
