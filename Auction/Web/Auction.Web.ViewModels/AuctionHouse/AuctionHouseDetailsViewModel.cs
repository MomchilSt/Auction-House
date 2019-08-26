using System.Collections.Generic;

namespace Auction.Web.ViewModels.AuctionHouse
{
    public class AuctionHouseDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public IEnumerable<AuctionHouseReviewViewModel> Reviews { get; set; }
    }
}
