﻿using Microsoft.AspNetCore.Mvc;
using Auction.Services.Interfaces;
using System.Collections.Generic;
using Auction.Web.ViewModels.Item;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace Auction.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string indexLoggedIn = "IndexLoggedIn";
        private readonly IItemService itemService;

        public HomeController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        public async Task<IActionResult> Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                List<ItemHomeViewModel> items = await this.itemService.GetAllItems()
                    .Select(x => new ItemHomeViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        StartingPrice = x.StartingPrice,
                        BuyOutPrice = x.BuyOutPrice,
                        EndDate = x.EndTime,
                        Picture = x.Picture
                    })
                    .OrderByDescending(x => x.EndDate)
                    .ToListAsync();

                return this.View(indexLoggedIn, items);
            }

            return View();
        }
    }
}
