﻿using Auction.Data;
using Auction.Data.Models;
using Auction.Data.Models.Enums;
using Auction.Services.Interfaces;
using Auction.Web.InputModels.Item;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Services.Services
{
    public class ItemService : IItemService
    {
        private const string itemNullException = "Error in creating item!";

        private readonly AuctionDbContext context;
        private readonly ICloudinaryService cloudinaryService;

        public ItemService(AuctionDbContext context, ICloudinaryService cloudinaryService)
        {
            this.context = context;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<bool> Create(ItemCreateInputModel inputModel)
        {
            var startTime = DateTime.UtcNow;
            var endTime = startTime.AddHours(inputModel.AuctionDuration);

            string pictureUrl = await this.cloudinaryService.UploadPictureAsync(
                inputModel.Picture,
                inputModel.Name);

            var auctionHouseFromDb = this.context.AuctionHouses
                .FirstOrDefault(x => x.Name == inputModel.AuctionHouse);

            Category category;

            if (!Enum.TryParse(inputModel.Category, out category) || auctionHouseFromDb == null)
            {
                throw new ArgumentNullException(itemNullException);
            }

            var item = new Item
            {
                Name = inputModel.Name,
                Category = category,
                StartingPrice = inputModel.StartingPrice,
                BuyOutPrice = inputModel.BuyOutPrice,
                Picture = pictureUrl,
                StartTime = startTime,
                EndTime = endTime,
                AuctionHouse = auctionHouseFromDb,
                Description = inputModel.Description
            };

            this.context.Items.Add(item);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<Item> GetAllItems()
        {
            var items = this.context.Items;

            return items;
        }

        public async Task<Item> GetById(string id)
        {
            return await this.context.Items
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Delete(string id)
        {
            var itemFromDb = this.context.Items.SingleOrDefault(x => x.Id == id);

            //TODO
            if (itemFromDb == null)
            {
                throw new ArgumentNullException(nameof(itemFromDb));
            }

            this.context.Items.Remove(itemFromDb);

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

    }
}
