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
    public class UserServiceTests
    {
        private IUserService userService;

        private List<AuctionUser> GetDummyData()
        {
            return new List<AuctionUser>
            {
                new AuctionUser
                {
                    FullName = "Asheley",
                    ItemsAuctioned =
                    {
                        new Item
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Bow",
                            Category = Category.Other,
                            StartingPrice = 13.13M,
                            BuyOutPrice = 34.33M,
                            StartTime = DateTime.UtcNow,
                            EndTime = DateTime.UtcNow.AddDays(1),
                            Description = "Legit",
                        }
                    }
                },

                new AuctionUser
                {
                    FullName = "Hose",
                    ItemsAuctioned =
                    {
                        new Item
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Spoon",
                            Category = Category.Other,
                            StartingPrice = 13.13M,
                            BuyOutPrice = 34.33M,
                            StartTime = DateTime.UtcNow,
                            EndTime = DateTime.UtcNow.AddDays(1),
                            Description = "Legit",
                        }
                    }
                }};

        }

        private async Task SeedData(AuctionDbContext context)
        {
            context.AddRange(GetDummyData());
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetById_WithExistingId_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "UserService GetById() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);

            var expectedData = context.Users.First();
            var actualData = await this.userService.GetById(expectedData.Id);

            Assert.True(expectedData.Id == actualData.Id, errorMessagePrefix + "Ids are different.");
            Assert.True(expectedData.FullName == actualData.FullName, errorMessagePrefix + "Full name is different.");
        }

        [Fact]
        public async Task GetById_WithNonExistingId_ShouldReturnNull()
        {
            string errorMessagePrefix = "UserService GetById() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);

            var actualData = await this.userService.GetById("Missing");

            Assert.True(actualData == null, errorMessagePrefix);
        }

        [Fact]
        public async Task Delete_WithCorrectData_ShouldDeleteSuccessfullyCreate()
        {
            string errorMessagePrefix = "UserService Delete() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);

            var dummyUser = context.Users.First();
            var dummyUsersItem = dummyUser.ItemsAuctioned.First().Name;

            bool actualResult = await this.userService.Delete(dummyUser.Id, dummyUsersItem);

            Assert.True(actualResult, errorMessagePrefix);
        }

        [Fact]
        public async Task Delete_WithWrongItemNameData_ShouldThrowException()
        {

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);

            var user = context.Users.First();

            var item = new Item
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Wooden axe",
                Category = Category.Other,
                StartingPrice = 13.13M,
                BuyOutPrice = 34.33M,
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddDays(1),
                Description = "Legit",
                AuctionUserId = user.Id
            };

            context.Items.Add(item);

            string missingItem = "Missing";

            await Assert.ThrowsAsync<ArgumentNullException>(() => this.userService.Delete(user.Id, missingItem));
        }

        [Fact]
        public async Task Edit_FullName_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "UserService Edit() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);

            var user = context.Users.First();

            var fullName = "Hugo";

            bool result = await this.userService.Edit(user.Id, fullName);

            Assert.True(result, errorMessagePrefix);
        }
    }
}
