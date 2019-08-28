using Auction.Services.Interfaces;
using Auction.Web.InputModels.Item;
using Auction.Web.ViewModels.Item.Delete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Web.Areas.Administration.Controllers
{
    public class ItemController : AdminController
    {
        public const string DeletePostRoute = "/Administration/Item/Delete/{id}";

        private readonly IItemService itemService;
        private readonly IAuctionHouseService auctionHouseService;

        public ItemController(IItemService itemService, IAuctionHouseService auctionHouseService)
        {
            this.itemService = itemService;
            this.auctionHouseService = auctionHouseService;
        }


        public async Task<IActionResult> Delete(string id)
        {
            var itemFromDb = await this.itemService.GetById(id);

            var auctionHouses = await auctionHouseService.GetAllAuctionHouses().ToListAsync();

            var viewModel = new ItemDeleteViewModel
            {
                Name = itemFromDb.Name,
                StartingPrice = itemFromDb.StartingPrice,
                BuyOutPrice = itemFromDb.BuyOutPrice,
                Picture = itemFromDb.Picture,
                AuctionDuration = itemFromDb.EndTime.ToString(),
                Category = new ItemDeleteCategoryViewModel
                {
                    Name = itemFromDb.Category.ToString()
                },
                Description = itemFromDb.Description,
                AuctionHouse = itemFromDb.AuctionHouse.Name
            };


            this.ViewData["auctionHouses"] = auctionHouses.Select(auctionHouse =>
            new ItemDeleteCategoryViewModel
            {
                Name = auctionHouse.Name
            })
                .ToList();

            return this.View(viewModel);
        }

        [HttpPost]
        [Route(DeletePostRoute)]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            await this.itemService.Delete(id);

            return this.RedirectToHome();
        }
    }
}
