using GlobalConstants;
using System.ComponentModel.DataAnnotations;

namespace Auction.Web.InputModels.City
{
    public class CityCreateInputModel
    {
        public const string CityNameError = "Name is required!";

        [Required(ErrorMessage = CityNameError)]
        [MaxLength(ModelConstants.City.CityNameMaxLength)]
        public string Name { get; set; }
    }
}
