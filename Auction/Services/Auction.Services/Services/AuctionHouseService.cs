﻿using Auction.Data;
using Auction.Data.Models;
using Auction.Services.Interfaces;
using Auction.Web.InputModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Services.Services
{
    public class AuctionHouseService : IAuctionHouseService
    {
        private readonly AuctionDbContext context;

        public AuctionHouseService(AuctionDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(AuctionHouseCreateInputModel inputModel)
        {
            City cityFromDb =
                context.Cities
                .FirstOrDefault(city => city.Name == inputModel.City);

            if (cityFromDb == null)
            {
                throw new ArgumentNullException(nameof(cityFromDb));
            }

            AuctionHouse auctionHouse = new AuctionHouse
            {
                Name = inputModel.Name,
                Address = inputModel.Address,
                Description = inputModel.Description,
            };

            auctionHouse.City = cityFromDb;

            context.AuctionHouses.Add(auctionHouse);
            int result = await context.SaveChangesAsync();

            return result > 0;
        }
    }
}
