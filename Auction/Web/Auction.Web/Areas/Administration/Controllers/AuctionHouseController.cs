using Auction.Services.Interfaces;
using Auction.Web.InputModels.AuctionHouse;
using Auction.Web.ViewModels.AuctionHouse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Web.Areas.Administration.Controllers
{
    public class AuctionHouseController : AdminController
    {
        private readonly IAuctionHouseService auctionHouseService;
        private readonly ICityService cityService;

        public AuctionHouseController(IAuctionHouseService auctionHouseService, ICityService cityService)
        {
            this.auctionHouseService = auctionHouseService;
            this.cityService = cityService;
        }

        public async Task<IActionResult> Create()
        {
            var allCities = await this.cityService.GetAllCities().ToListAsync();

            this.ViewData["cities"] = allCities.Select(cities => 
            new AuctionHouseCreateCityViewModel
            {
                Name = cities.Name
            })
                .ToList();

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuctionHouseCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                var allCities = await this.cityService.GetAllCities().ToListAsync();

                this.ViewData["cities"] = allCities.Select(cities =>
                new AuctionHouseCreateCityViewModel
                {
                    Name = cities.Name
                })
                    .ToList();


                return this.View();
            }
         

            await this.auctionHouseService.Create(inputModel);

            return RedirectToHome();
        }
    }
}