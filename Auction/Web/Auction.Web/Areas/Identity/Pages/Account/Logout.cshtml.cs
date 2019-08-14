using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Auction.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Auction.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AuctionUser> _signInManager;

        public LogoutModel(SignInManager<AuctionUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            await _signInManager.SignOutAsync();

            return Redirect("/Identity/Account/Login");
        }

        //public async Task<IActionResult> OnPost(string returnUrl = null)
        //{

        //    _logger.LogInformation("User logged out.");
        //    if (returnUrl != null)
        //    {
        //        return LocalRedirect(returnUrl);
        //    }
        //    else
        //    {
        //        return Page();
        //    }
        //}
    }
}