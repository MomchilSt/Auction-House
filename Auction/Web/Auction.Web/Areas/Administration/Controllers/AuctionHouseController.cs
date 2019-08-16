using Auction.Services.Interfaces;
using Auction.Web.InputModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Auction.Web.Areas.Administration.Controllers
{
    public class AuctionHouseController : AdminController
    {
        private readonly IAuctionHouseService auctionHouseService;

        public AuctionHouseController(IAuctionHouseService auctionHouseService)
        {
            this.auctionHouseService = auctionHouseService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuctionHouseCreateInputModel inputModel)
        {
            await this.auctionHouseService.Create(inputModel);

            return this.Redirect("/");
        }
    }
}