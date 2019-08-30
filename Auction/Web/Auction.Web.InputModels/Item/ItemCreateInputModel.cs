using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using GlobalConstants;

namespace Auction.Web.InputModels.Item
{
    public class ItemCreateInputModel
    {
        [Required]
        [MaxLength(ModelConstants.Item.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [Range(typeof(decimal), ModelConstants.Item.MinPrice, ModelConstants.Item.MaxPrice)]
        public decimal StartingPrice { get; set; }

        [Required]
        [Range(typeof(decimal), ModelConstants.Item.MinPrice, ModelConstants.Item.MaxPrice)]
        public decimal BuyOutPrice { get; set; }

        [Required]
        public IFormFile Picture { get; set; }

        [Required]
        [Range(ModelConstants.Item.MinDuration, ModelConstants.Item.MaxDuration)]
        public int AuctionDuration { get; set; }

        [Required]
        public string AuctionHouse { get; set; }

        [Required]
        [MaxLength(ModelConstants.Item.DescriptionMaxLength)]
        public string Description { get; set; }
    }
}
