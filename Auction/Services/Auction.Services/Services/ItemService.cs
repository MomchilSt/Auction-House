using Auction.Data;
using Auction.Data.Models;
using Auction.Data.Models.Enums;
using Auction.Services.Interfaces;
using Auction.Web.InputModels.Item;
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
            var startTime = DateTime.Now;
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
    }
}
