using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auction.Services.Interfaces;
using Auction.Web.InputModels.Item;
using Auction.Web.ViewModels.Item;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auction.Web.Controllers
{
    public class ItemController : BaseController
    {
        private readonly IAuctionHouseService auctionHouseService;
        private readonly IItemService itemService;

        public ItemController(IAuctionHouseService auctionHouseService, IItemService itemService)
        {
            this.auctionHouseService = auctionHouseService;
            this.itemService = itemService;
        }

        public async Task<IActionResult> Create()
        {
            var auctionHouses = await auctionHouseService.GetAllAuctionHouses().ToListAsync();

            this.ViewData["auctionHouses"] = auctionHouses.Select(auctionHouse =>
            new ItemCreateAuctionHouseViewModel
            {
                Name = auctionHouse.Name
            })
                .ToList();

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemCreateInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                var auctionHouses = await auctionHouseService.GetAllAuctionHouses().ToListAsync();

                this.ViewData["auctionHouses"] = auctionHouses.Select(auctionHouse =>
                new ItemCreateAuctionHouseViewModel
                {
                    Name = auctionHouse.Name
                })
                    .ToList();

                return this.View();
            }

            await this.itemService.Create(inputModel);

            return this.Redirect("/");
        }
    }
}