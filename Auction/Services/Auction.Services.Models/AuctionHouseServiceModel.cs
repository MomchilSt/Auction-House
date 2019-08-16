namespace Auction.Services.Models
{
    public class AuctionHouseServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string CityId { get; set; }
        public CityServiceModel City { get; set; }
    }
}
