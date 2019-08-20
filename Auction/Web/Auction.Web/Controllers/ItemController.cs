using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Web.Controllers
{
    public class ItemController : BaseController
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}