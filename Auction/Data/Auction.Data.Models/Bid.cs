using GlobalConstants;
using System;
using System.ComponentModel.DataAnnotations;

namespace Auction.Data.Models
{
    public class Bid
    {
        public string Id { get; set; }

        [Required]
        [Range(typeof(decimal), ModelConstants.Bid.MinPrice, ModelConstants.Bid.MaxPrice)]
        public decimal Amount { get; set; }

        [Required]
        public string ItemId { get; set; }
        public Item Item { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
