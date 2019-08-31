using Auction.Data;
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
        private readonly AuctionDbContext context;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IUserService userService;
        private readonly IReceiptService receiptService;

        public ItemService(AuctionDbContext context,
            ICloudinaryService cloudinaryService,
            IUserService userService,
            IReceiptService receiptService)
        {
            this.context = context;
            this.cloudinaryService = cloudinaryService;
            this.userService = userService;
            this.receiptService = receiptService;
        }

        public async Task<bool> Create(ItemCreateInputModel inputModel, string ownerId)
        {
            var startTime = DateTime.UtcNow;
            var endTime = startTime.AddMinutes(inputModel.AuctionDuration);

            string pictureUrl = await this.cloudinaryService
                .UploadPictureAsync(
                inputModel.Picture,
                inputModel.Name);

            var auctionHouseFromDb = this.context
                .AuctionHouses
                .FirstOrDefault(x => x.Name == inputModel.AuctionHouse);

            Category category;

            if (!Enum.TryParse(inputModel.Category, out category) || auctionHouseFromDb == null)
            {
                throw new ArgumentNullException(nameof(auctionHouseFromDb));
            }

            var owner = await this.userService.GetById(ownerId);

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
                Description = inputModel.Description,
            };

            item.AuctionUser = owner;
            item.Status = ItemStatus.InAction;
            this.context.Items.Add(item);

            int result = await this.context
                .SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<Item> GetAllItems()
        {
            var items = this.context.Items;

            return items;
        }

        public async Task<Item> GetById(string id)
        {
            var itemFromDb = await this.context.
                Items
                .Include(x => x.Bids)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (itemFromDb == null)
            {
                throw new ArgumentNullException(nameof(itemFromDb));
            }

            return itemFromDb;
        }

        public async Task<Item> GetByName(string name)
        {
            var itemFromDb = await this.context
                .Items
                .SingleOrDefaultAsync(x => x.Name == name);

            if (itemFromDb == null)
            {
                throw new ArgumentNullException(nameof(itemFromDb));
            }

            return itemFromDb;
        }

        public async Task<bool> Delete(string id)
        {
            var itemFromDb = this.context
                .Items
                .SingleOrDefault(x => x.Id == id);

            if (itemFromDb == null)
            {
                throw new ArgumentNullException(nameof(itemFromDb));
            }

            this.context
                .Items.
                Remove(itemFromDb);

            int result = await this.context
                .SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Buy(string id, string ownerId)
        {
            var itemFromDb = await this.context
                .Items
                .SingleOrDefaultAsync(x => x.Id == id);

            if (itemFromDb == null)
            {
                throw new ArgumentNullException(nameof(itemFromDb));
            }

            if (itemFromDb.EndTime < DateTime.UtcNow)
            {
                return false;
            }

            var user = await this.userService.GetById(ownerId);

            if (itemFromDb.EndTime > DateTime.UtcNow)
            {
                itemFromDb.EndTime = DateTime.UtcNow;
            }

            itemFromDb.Status = ItemStatus.Bought;

            bool isReceiptCreated = await this.receiptService.CreateReceipt(itemFromDb.Id, ownerId);

            return isReceiptCreated;
        }
    }
}
