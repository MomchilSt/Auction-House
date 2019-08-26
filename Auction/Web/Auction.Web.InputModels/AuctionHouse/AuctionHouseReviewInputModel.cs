using System.ComponentModel.DataAnnotations;

namespace Auction.Web.InputModels.AuctionHouse
{
    public class AuctionHouseReviewInputModel
    {
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
