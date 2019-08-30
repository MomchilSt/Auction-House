using GlobalConstants;
using System.ComponentModel.DataAnnotations;

namespace Auction.Web.InputModels.User
{
    public class ProfileEditInputModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(ModelConstants.User.FullNameMaxLength)]
        public string FullName { get; set; }
    }
}
