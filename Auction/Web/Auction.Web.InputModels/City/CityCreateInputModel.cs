using System.ComponentModel.DataAnnotations;

namespace Auction.Web.InputModels.City
{
    public class CityCreateInputModel
    {
        public const string CityNameError = "Name is required!";

        [Required(ErrorMessage = CityNameError)]
        public string Name { get; set; }
    }
}
