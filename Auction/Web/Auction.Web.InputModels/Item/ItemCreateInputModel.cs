using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Auction.Web.InputModels.Item
{
    public class ItemCreateInputModel
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public decimal StartingPrice { get; set; }

        public decimal BuyOutPrice { get; set; }

        [Required]
        public IFormFile Picture { get; set; }

        public int AuctionDuration { get; set; }

        public string AuctionHouse { get; set; }

        public string Description { get; set; }
    }
}
