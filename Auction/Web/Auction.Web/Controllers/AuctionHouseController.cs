using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auction.Services.Interfaces;
using Auction.Web.InputModels.AuctionHouse;
using Auction.Web.ViewModels.AuctionHouse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auction.Web.Controllers
{
    public class AuctionHouseController : BaseController
    {
        private readonly IAuctionHouseService auctionHouseService;
        private readonly IUserService userService;

        public AuctionHouseController(IAuctionHouseService auctionHouseService, IUserService userService)
        {
            this.auctionHouseService = auctionHouseService;
            this.userService = userService;
        }

        public async Task<IActionResult> Details(string id)
        {
            var auctionHouseFromDb = await this.auctionHouseService.GetById(id);

            if (auctionHouseFromDb == null)
            {
                //TODO: CHECK 
                throw new ArgumentNullException(nameof(auctionHouseFromDb));
            }

            var reviewsModel = auctionHouseFromDb.Reviews.Select(auctionHouse => new AuctionHouseReviewViewModel
            {
                Id = auctionHouse.Id,
                Author = auctionHouse.Author,
                Description = auctionHouse.Description,
            });

            var viewModel = new AuctionHouseDetailsViewModel
            {
                Name = auctionHouseFromDb.Name,
                Address = auctionHouseFromDb.Address,
                Description = auctionHouseFromDb.Description,
                Reviews = reviewsModel
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Review()
        {
            var allAuctionHouses = await this.auctionHouseService.GetAllAuctionHouses().ToListAsync();

            this.ViewData["auctionHouses"] = allAuctionHouses.Select(cities =>
            new AuctionHouseNameViewModel
            {
                Name = cities.Name
            })
                .ToList();

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Review(AuctionHouseReviewInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                var allAuctionHouses = await this.auctionHouseService.GetAllAuctionHouses().ToListAsync();

                this.ViewData["auctionHouses"] = allAuctionHouses.Select(cities =>
                new AuctionHouseNameViewModel
                {
                    Name = cities.Name
                })
                    .ToList();

                return this.View();
            }

            var auctionHouseFromDb = await this.auctionHouseService.GetByName(inputModel.AuctionHouseName);

            if (auctionHouseService == null)
            {
                throw new ArgumentNullException(nameof(auctionHouseFromDb));
            }

            var authorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var author = await this.userService.GetById(authorId);

            await this.auctionHouseService.CreateReview(auctionHouseFromDb.Id, author.UserName, inputModel);

            return this.RedirectToHome();
        }
    }
}