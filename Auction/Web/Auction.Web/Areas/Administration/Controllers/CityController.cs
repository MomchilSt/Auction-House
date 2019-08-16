using Auction.Data.Models;
using Auction.Services.Interfaces;
using Auction.Web.InputModels;
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

        public async Task<IActionResult> CreateCity()
        {
            return View();
        }

        public async Task<IActionResult> CreateCity(CityCreateInputModel cityCreateInputModel)
        {
            await this.cityService.CreateCity(cityCreateInputModel);

            return this.Redirect("/");
        }
    }
}