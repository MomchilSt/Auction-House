using System.ComponentModel.DataAnnotations;

namespace Auction.Data.Models
{
    public class BaseModel<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}
