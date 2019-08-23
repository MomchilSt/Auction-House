using Auction.Data.Models;
using Auction.Services.Interfaces;
using Auction.Web.InputModels.City;
using GlobalConstants;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Auction.Web.Areas.Administration.Controllers
{
    public class CityController : AdminController
    {
        private readonly ICityService cityService;

        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CityCreateInputModel cityCreateInputModel)
        {
            await this.cityService.CreateCity(cityCreateInputModel);

            return this.Redirect("/Administration/AuctionHouse/Create");
        }
    }
}