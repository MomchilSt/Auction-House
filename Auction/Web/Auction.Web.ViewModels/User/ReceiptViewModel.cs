using System;

namespace Auction.Web.ViewModels.User
{
    public class ReceiptViewModel
    {
        public DateTime IssuedOn { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal StartingPrice { get; set; }

        public decimal BuyOutPrice { get; set; }

        public DateTime StartTime { get; set; }
    }
}
