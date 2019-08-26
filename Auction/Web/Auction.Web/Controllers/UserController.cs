using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auction.Data.Models;
using Auction.Services.Interfaces;
using Auction.Web.InputModels.User;
using Auction.Web.ViewModels.User;
using GlobalConstants;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> ProfileDetails()
        {
            var id = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            AuctionUser user = await this.userService.GetById(id);

            IEnumerable<ProfileItemViewModel> items = user.ItemsAuctioned.Select(item => new ProfileItemViewModel
            {
                Name = item.Name,
                Picture = item.Picture
            });

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
                //TODO ERROR
                return this.View();
            }

            await this.userService.Edit(inputModel.Id, inputModel.FullName);

            var userFromDb = await this.userService.GetById(inputModel.Id);

            var itemsSold = userFromDb.ItemsAuctioned.Select(item => new ProfileItemViewModel
            {
                Name = item.Name,
                Picture = item.Picture
            });

            var viewModel = new ProfileDetailsViewModel
            {
                Id = userFromDb.Id,
                Username = userFromDb.UserName,
                FullName = userFromDb.FullName,
                ItemsAuctioned = itemsSold
            };

            return this.View(BasicConstants.ProfileRoute, viewModel);
        }
    }
}