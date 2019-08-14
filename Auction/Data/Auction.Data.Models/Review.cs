namespace Auction.Data.Models
{
    public class Review : BaseModel<string>
    {
        public string Author { get; set; }

        public string Description { get; set; }

        public decimal Rating { get; set; }
    }
}
