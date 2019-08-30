using Auction.Data.Models;
using Auction.Web.ViewModels;
using GlobalConstants;
using System.ComponentModel.DataAnnotations;

namespace Auction.Web.InputModels.AuctionHouse
{
    public class AuctionHouseCreateInputModel
    {
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
        public string City { get; set; }
    }
}
