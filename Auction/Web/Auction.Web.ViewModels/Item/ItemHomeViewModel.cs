using System;

namespace Auction.Web.ViewModels.Item
{
    public class ItemHomeViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal StartingPrice { get; set; }

        public decimal BuyOutPrice { get; set; }

        public DateTime EndDate { get; set; }

        public string Picture { get; set; }
    }
}
