using Auction.Data.Models.Enums;
using Auction.Services.Interfaces;
using Auction.Web.InputModels.Item;
using Auction.Web.ViewModels.Item.Admin;
using Auction.Web.ViewModels.Item.Delete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Web.Areas.Administration.Controllers
{
    public class ItemController : AdminController
    {
        public const string DeletePostRoute = "/Administration/Item/Delete/{id}";
        public const string ManagementRoute = "Management";
        private const string ErrorRoute = "/Error/Error";

        private readonly IItemService itemService;
        private readonly IAuctionHouseService auctionHouseService;

        public ItemController(IItemService itemService, IAuctionHouseService auctionHouseService)
        {
            this.itemService = itemService;
            this.auctionHouseService = auctionHouseService;
        }

        public async Task<IActionResult> Management()
        {
            List<ItemManagementViewModel> items = await this.itemService.GetAllItems()
                .Where(x => x.Status == ItemStatus.BidedOn)
                .Where(x => x.EndTime < DateTime.UtcNow)
                .Select(x => new ItemManagementViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    HighestBid = x.Bids
                    .Max(m => m.Amount)
                    .ToString("F2"),
                    BidderId = x.Bids
                    .OrderByDescending(o => o.Amount)
                    .First()
                    .UserId                    
                })
                .ToListAsync();

            return this.View(items);
        }

        [HttpPost]
        public async Task<IActionResult> Assign(string id, string bidderId)
        {
            bool isReceiptCreated = await this.itemService.Buy(id, bidderId);

            if (!isReceiptCreated)
            {
                return this.Redirect(ErrorRoute);
            }

            return this.RedirectToAction(ManagementRoute);
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
