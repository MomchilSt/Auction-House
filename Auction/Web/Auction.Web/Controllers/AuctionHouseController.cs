using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auction.Services.Interfaces;
using Auction.Web.InputModels.AuctionHouse;
using Auction.Web.ViewModels.AuctionHouse;
using Microsoft.AspNetCore.Mvc;

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
                Username = auctionHouse.Author,
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
    }
}