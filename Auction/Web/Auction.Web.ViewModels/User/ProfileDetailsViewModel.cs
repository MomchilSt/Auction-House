using System.Collections;
using System.Collections.Generic;

namespace Auction.Web.ViewModels.User
{
    public class ProfileDetailsViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public List<ProfileItemViewModel> ItemsAuctioned { get; set; }
    }
}
