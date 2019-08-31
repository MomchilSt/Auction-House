using GlobalConstants;
using System.ComponentModel.DataAnnotations;

namespace Auction.Data.Models
{
    public class City
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(ModelConstants.City.CityNameMaxLength)]
        public string Name { get; set; }
    }
}
