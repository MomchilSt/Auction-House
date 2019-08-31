using Auction.Data;
using Auction.Data.Models;
using Auction.Data.Models.Enums;
using Auction.Services.Interfaces;
using Auction.Services.Services;
using Auction.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Auction.Tests.Services
{
    public class ReceiptServiceTests
    {
        private IReceiptService receiptService;

        private AuctionUser SeedUser()
        {
            return new AuctionUser
            {
                Id = "Hugo"
            };
        }

        private List<Item> GetDummyData()
        {
            return new List<Item>
            {
                new Item
                {
                    Id = "SpoonId",
                    Name = "Spoon",
                    Category = Category.Other,
                    StartingPrice = 13.13M,
                    BuyOutPrice = 34.33M,
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddDays(1),
                    Description = "Legit",
                },

                new Item
                {
                    Id = "EyePatchId",
                    Name = "Eye patch",
                    Category = Category.Other,
                    StartingPrice = 13.13M,
                    BuyOutPrice = 34.33M,
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddDays(1),
                    Description = "Legit",
                }};

        }

        private List<Receipt> GetDummyReceipts()
        {
            return new List<Receipt>
            {
                new Receipt
            {
                ItemId = "EyePatchId",
                UserId = "Hugo"
            },

                new Receipt
            {
                ItemId = "SpoonId",
                UserId = "Hugo"
            }};

        }

        private async Task SeedData(AuctionDbContext context)
        {
            context.Users.Add(SeedUser());
            context.AddRange(GetDummyData());
            context.AddRange(GetDummyReceipts());
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task ReceiptCreate_WithCorrectData_ShouldReturnSuccessfullyCreate()
        {
            string errorMessagePrefix = "ReceiptService ReceiptCreate() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.receiptService = new ReceiptService(context);

            var dummyUser = new AuctionUser
            {
                FullName = "Pesho"
            };
            var dummyItemId = context.Items.First().Id;

            context.Users.Add(dummyUser);

            bool actualResult = await this.receiptService.CreateReceipt(dummyItemId, dummyUser.Id);

            Assert.True(actualResult, errorMessagePrefix);
        }

        [Fact]
        public async Task ReceiptCreate_WithWrongMissingItemIdData_ShouldReturnNull()
        {
            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.receiptService = new ReceiptService(context);

            var dummyUser = new AuctionUser
            {
                FullName = "Pesho"
            };
            var dummyItemId = "Missing";

            context.Users.Add(dummyUser);

            await Assert.ThrowsAsync<ArgumentNullException>(() => this.receiptService.CreateReceipt(dummyItemId, dummyUser.Id));
        }

        [Fact]
        public async Task GetReceiptsById_WithCorrectId_ShouldCorrectResults()
        {
            string errorMessagePrefix = "ReceiptService GetReceiptsById does not work properly.";
            string userId = "Hugo";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.receiptService = new ReceiptService(context);

            var actualReceipts = await this.receiptService.GetReceiptsById(userId).ToListAsync();
            var expectedReceipts = GetDummyReceipts();

            for (int i = 0; i < expectedReceipts.Count; i++)
            {
                Assert.True(expectedReceipts[0].ItemId == actualReceipts[0].ItemId, errorMessagePrefix + "Item ids are different.");
                Assert.True(expectedReceipts[0].UserId == actualReceipts[0].UserId, errorMessagePrefix + "User ids are different.");
            }
        }

        [Fact]
        public async Task GetReceiptsById_MissingId_ShouldReturnZeroResults()
        {
            string errorMessagePrefix = "ReceiptService GetReceiptsById does not work properly.";
            string userId = "Missing";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.receiptService = new ReceiptService(context);

            var actualReceipts = await this.receiptService.GetReceiptsById(userId).ToListAsync();

            Assert.True(actualReceipts.Count == 0, errorMessagePrefix);
        }
    }
}
