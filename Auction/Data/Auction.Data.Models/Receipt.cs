using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Auction.Data.Models
{
    public class Receipt
    {
        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        [Required]
        public string ItemId { get; set; }

        public Item Item { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
