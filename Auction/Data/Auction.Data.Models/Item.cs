using Auction.Data.Models.Enums;
using GlobalConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Auction.Data.Models
{
    public class Item
    {
        public Item()
        {
            this.Bids = new HashSet<Bid>();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(ModelConstants.Item.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ModelConstants.Item.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public Category Category { get; set; }

        public ItemStatus Status { get; set; }

        [Required]
        [Range(typeof(decimal), ModelConstants.Item.MinPrice, ModelConstants.Item.MaxPrice)]
        public decimal StartingPrice { get; set; }

        [Required]
        [Range(typeof(decimal), ModelConstants.Item.MinPrice, ModelConstants.Item.MaxPrice)]
        public decimal BuyOutPrice { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public string AuctionUserId { get; set; }

        public AuctionUser AuctionUser { get; set; }

        [Required]
        public string AuctionHouseId { get; set; }
        public AuctionHouse AuctionHouse { get; set; }

        public string Picture { get; set; }

        public ICollection<Bid> Bids { get; set; }
    }
}
