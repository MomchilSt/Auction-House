using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auction.Data.Models;
using Auction.Services.Interfaces;
using Auction.Web.InputModels.Item;
using Auction.Web.InputModels.User;
using Auction.Web.ViewModels.Item;
using Auction.Web.ViewModels.User;
using GlobalConstants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Auction.Web.Controllers
{
    public class UserController : BaseController
    {
        private const string ProfileDetailsAction = "ProfileDetails";

        private readonly IUserService userService;
        private readonly IReceiptService receiptService;

        public UserController(IUserService userService, IReceiptService receiptService)
        {
            this.userService = userService;
            this.receiptService = receiptService;
        }

        public async Task<IActionResult> ProfileDetails()
        {
            var id = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            AuctionUser user = await this.userService.GetById(id);

            List<ProfileItemViewModel> items = user.ItemsAuctioned.Select(item => new ProfileItemViewModel
            {
                Name = item.Name,
                Picture = item.Picture
            }).ToList();

            var viewModel = new ProfileDetailsViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                FullName = user.FullName,
                ItemsAuctioned = items
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileEditInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            await this.userService.Edit(inputModel.Id, inputModel.FullName);

            var userFromDb = await this.userService.GetById(inputModel.Id);

            List<ProfileItemViewModel> itemsSold = userFromDb.ItemsAuctioned.Select(item => new ProfileItemViewModel
            {
                Name = item.Name,
                Picture = item.Picture
            }).ToList();

            var viewModel = new ProfileDetailsViewModel
            {
                Id = userFromDb.Id,
                Username = userFromDb.UserName,
                FullName = userFromDb.FullName,
                ItemsAuctioned = itemsSold
            };

            return this.View(BasicConstants.ProfileRoute, viewModel);
        }

        public async Task<IActionResult> Receipts()
        {
            var id = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var receiptsFromDb = await this.receiptService.GetReceiptsById(id).ToListAsync();



            List<ReceiptViewModel> viewModel = receiptsFromDb.Select(x => new ReceiptViewModel
            {
                IssuedOn = x.IssuedOn,
                Name = x.Item.Name,
                Description = x.Item.Description,
                StartingPrice = x.Item.StartingPrice,
                BuyOutPrice = x.Item.BuyOutPrice,
                StartTime = x.Item.StartTime,
            })
                .ToList();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string name)
        {
            var id = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            AuctionUser user = await this.userService.GetById(id);

            await this.userService.Delete(user.Id, name);

            return this.RedirectToAction(ProfileDetailsAction);
        }
    }
}