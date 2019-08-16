using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = BasicConstants.Admin)]
    [Area(BasicConstants.AdministrationArea)]
    public abstract class AdminController : Controller
    {
    }
}