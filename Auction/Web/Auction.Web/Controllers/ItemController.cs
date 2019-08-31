using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auction.Data.Models;
using Auction.Services.Interfaces;
using Auction.Web.InputModels.Item;
using Auction.Web.ViewModels.Item;
using GlobalConstants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auction.Web.Controllers
{
    public class ItemController : BaseController
    {
        private const string DetailsRoute = "Details";
        private const string ErrorRoute = "/Error/Error";

        private readonly IAuctionHouseService auctionHouseService;
        private readonly IItemService itemService;
        private readonly IBidService bidService;

        public ItemController(IAuctionHouseService auctionHouseService,
            IItemService itemService,
            IBidService bidService)
        {
            this.auctionHouseService = auctionHouseService;
            this.itemService = itemService;
            this.bidService = bidService;
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
            if (!ModelState.IsValid || inputModel.StartingPrice > inputModel.BuyOutPrice)
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

             var ownerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.itemService.Create(inputModel, ownerId);

            return this.RedirectToHome();
        }

        public async Task<IActionResult> Details(string id)
        {
            var itemFromDb = await this.itemService.GetById(id);


            if (itemFromDb == null)
            {
                return this.Redirect(ErrorRoute);
            }

            var auctionHouseFromDb = await this.auctionHouseService.GetById(itemFromDb.AuctionHouseId);
            var firstItemBidFromDb = itemFromDb.Bids.FirstOrDefault();

            if (firstItemBidFromDb == null)
            {
                ItemDetailsViewModel itemWithNoBids = new ItemDetailsViewModel
                {
                    Id = itemFromDb.Id,
                    Name = itemFromDb.Name,
                    Description = itemFromDb.Description,
                    StartingPrice = itemFromDb.StartingPrice,
                    BuyOutPrice = itemFromDb.BuyOutPrice,
                    StartTime = itemFromDb.StartTime,
                    EndTime = itemFromDb.EndTime,
                    Picture = itemFromDb.Picture,
                    AuctionHouse = new ItemDetailsAuctionHouseViewModel
                    {
                        Id = auctionHouseFromDb.Id,
                        Name = auctionHouseFromDb.Name,
                        Address = auctionHouseFromDb.Address
                    }
                };

                return this.View(itemWithNoBids);
            }

            ItemDetailsViewModel item = new ItemDetailsViewModel
            {
                Id = itemFromDb.Id,
                Name = itemFromDb.Name,
                Description = itemFromDb.Description,
                StartingPrice = itemFromDb.StartingPrice,
                BuyOutPrice = itemFromDb.BuyOutPrice,
                StartTime = itemFromDb.StartTime,
                EndTime = itemFromDb.EndTime,
                Picture = itemFromDb.Picture,
                HighestBid = itemFromDb.Bids
                .Max(x => x.Amount)
                .ToString("F2"),
                AuctionHouse = new ItemDetailsAuctionHouseViewModel
                {
                    Id = auctionHouseFromDb.Id,
                    Name = auctionHouseFromDb.Name,
                    Address = auctionHouseFromDb.Address
                }
            };

            return this.View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Buy(string id)
        {
            var ownerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (ownerId == null)
            {
                return this.Redirect(ErrorRoute);
            }


            var isBought = await this.itemService.Buy(id, ownerId);

            if (!isBought)
            {
                return this.Redirect(DetailsRoute + "/" + id);
            }

            return RedirectToHome();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBid(string Id, string amount)
        {
            if (amount == null)
            {
                return this.Redirect(DetailsRoute + "/" + Id);
            }

            decimal amountParsed;

            if (!(Decimal.TryParse(amount, out amountParsed)))
            {
                return this.Redirect(DetailsRoute + "/" + Id);
            }

            var item = await this.itemService.GetById(Id);

            var bidderId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var isBidCreated = await this.bidService.CreateBid(item, bidderId, amountParsed);

            if (!isBidCreated)
            {
                return this.Redirect(ErrorRoute);
            }

            return this.Redirect(DetailsRoute + "/" + Id);
        }
    }
}   