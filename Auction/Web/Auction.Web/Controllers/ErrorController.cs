using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("error/404")]
        public IActionResult Error404()
        {
            return this.View();
        }

        [Route("error/403")]
        public IActionResult Error403()
        {
            return this.View();
        }

        [Route("error/{code:int}")]
        public IActionResult Error(int code)
        {
            return this.View();
        }
    }
}