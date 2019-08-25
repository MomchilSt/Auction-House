using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected void ShowErrorMessage(string message)
        {
            this.TempData[BasicConstants.TempErrorMessage] = message;
        }

        protected void ShowSuccessMessage(string message)
        {
            this.TempData[BasicConstants.TempSuccessMessage] = message;
        }

        protected IActionResult RedirectToHome() => this.Redirect("/");
    }
}