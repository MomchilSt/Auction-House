using Auction.Data;
using Auction.Data.Models;
using Auction.Data.Models.Enums;
using Auction.Services.Interfaces;
using Auction.Services.Services;
using Auction.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Auction.Tests.Services
{
    public class BidServiceTests
    {
        private IBidService bidService;

        private IUserService userService;

        private AuctionUser GetDummyUser()
        {
            return new AuctionUser
            {
                Id = "Hugo"
            };
        }

        private City GetDummyCity()
        {
            return new City
            {
                Id = "QmbolId",
                Name = "Qmbol"
            };
        }

        private AuctionHouse GetDummyAuctionHouse()
        {
            return new AuctionHouse
            {
                Id = "EmberId",
                Name = "Ember",
                Address = "Wholesome street",
                Description = "Legit",
                CityId = "QmbolId"
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
                }

            };
        }
        private async Task SeedData(AuctionDbContext context)
        {
            context.Users.Add(GetDummyUser());
            context.Cities.Add(GetDummyCity());
            context.AuctionHouses.Add(GetDummyAuctionHouse());
            context.AddRange(GetDummyData());
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task CreateBid_WithCorrectParams_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "BidService CreateBid() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);
            this.bidService = new BidService(context, userService);

            var dummyUserId = "Hugo";
            var dummyItem = context.Items.First();
            var result = await this.bidService.CreateBid(dummyItem, dummyUserId, 25);

            Assert.True(result, errorMessagePrefix);
        }

        [Fact]
        public async Task CreateBid_WithIncorrectMissingBidderId_ShouldReturnFalse()
        {
            string errorMessagePrefix = "BidService CreateBid() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);
            this.bidService = new BidService(context, userService);

            var dummyUserId = "Missing";
            var dummyItem = context.Items.First();
            var result = await this.bidService.CreateBid(dummyItem, dummyUserId, 25);

            Assert.False(result, errorMessagePrefix);
        }

        [Fact]
        public async Task CreateBid_WithIncorrectItem_ShouldReturnFaslse()
        {
            string errorMessagePrefix = "BidService CreateBid() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);
            this.bidService = new BidService(context, userService);


            var dummyUserId = "Hugo";
            var dummyItem = context.Items.FirstOrDefault(x => x.Name == "missing");
            var result = await this.bidService.CreateBid(dummyItem, dummyUserId, 25);

            Assert.False(result, errorMessagePrefix);
        }

        [Fact]
        public async Task CreateBid_WithIncorrectAmount_ShouldReturnFaslse()
        {
            string errorMessagePrefix = "BidService CreateBid() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);
            this.bidService = new BidService(context, userService);


            var dummyUserId = "Hugo";
            var dummyItem = context.Items.First();
            var result = await this.bidService.CreateBid(dummyItem, dummyUserId, 2);

            Assert.False(result, errorMessagePrefix);
        }

        [Fact]
        public async Task CreateBid_WithIncorrectBiddingTime_ShouldReturnFaslse()
        {
            string errorMessagePrefix = "BidService CreateBid() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);
            this.bidService = new BidService(context, userService);

            var item = new Item
            {
                Id = "BoneId",
                Name = "Bone",
                Category = Category.Other,
                StartingPrice = 13.13M,
                BuyOutPrice = 34.33M,
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddMinutes(-1),
                Description = "Legit",
            };

            context.Items.Add(item);
            context.SaveChanges();

            var dummyUserId = "Hugo";
            var dummyItem = context.Items.First(x => x.Name == "Bone");
            var result = await this.bidService.CreateBid(dummyItem, dummyUserId, 20);

            Assert.False(result, errorMessagePrefix);
        }
    }
}