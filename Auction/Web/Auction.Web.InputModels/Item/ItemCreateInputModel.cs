using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Auction.Web.InputModels.Item
{
    public class ItemCreateInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public decimal StartingPrice { get; set; }

        [Required]
        public decimal BuyOutPrice { get; set; }

        [Required]
        public IFormFile Picture { get; set; }

        [Required]
        public int AuctionDuration { get; set; }

        [Required]
        public string AuctionHouse { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
