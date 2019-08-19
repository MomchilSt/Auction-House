using Auction.Data.Models;
using Auction.Web.ViewModels;

namespace Auction.Web.InputModels
{
    public class AuctionHouseCreateInputModel
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string City { get; set; }
    }
}
