using Auction.Data;
using Auction.Data.Models;
using Auction.Data.Models.Enums;
using Auction.Services.Interfaces;
using Auction.Services.Services;
using Auction.Tests.Common;
using Auction.Web.InputModels.Item;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Auction.Tests.Services
{
    public class itemServiceTests
    {
        private IItemService itemService;

        private ICloudinaryService cloudinaryService;
        private IUserService userService;
        private IReceiptService receiptService;
        private readonly Cloudinary cloudinaryUtility;

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
                }};
        }
        private async Task SeedData(AuctionDbContext context)
        {
            context.Cities.Add(GetDummyCity());
            context.AuctionHouses.Add(GetDummyAuctionHouse());
            context.AddRange(GetDummyData());
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task Delete_WithCorrectId_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ItemService Delete() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);
            this.receiptService = new ReceiptService(context);
            this.cloudinaryService = new CloudinaryService(cloudinaryUtility);
            this.itemService = new ItemService(context, cloudinaryService, userService, receiptService);

            var itemId = "SpoonId";

            bool actualResult = await this.itemService.Delete(itemId);

            Assert.True(actualResult, errorMessagePrefix);
        }

        [Fact]
        public async Task Delete_WithNonExcistantId_ShouldThrowException()
        {
            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);
            this.receiptService = new ReceiptService(context);
            this.cloudinaryService = new CloudinaryService(cloudinaryUtility);
            this.itemService = new ItemService(context, cloudinaryService, userService, receiptService);

            var itemId = "Missing";

            await Assert.ThrowsAsync<ArgumentNullException>(() => this.itemService.Delete(itemId));
        }

        [Fact]
        public async Task Buy_WithNonExcistantItemId_ShouldThrowException()
        {
            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);
            this.receiptService = new ReceiptService(context);
            this.cloudinaryService = new CloudinaryService(cloudinaryUtility);
            this.itemService = new ItemService(context, cloudinaryService, userService, receiptService);

            var dummyItemId = "Missing";

            var dummyUser = new AuctionUser
            {
                Id = "Hugo"
            };

            context.Users.Add(dummyUser);

            await Assert.ThrowsAsync<ArgumentNullException>(() => this.itemService.Buy(dummyItemId, dummyUser.Id));
        }

        [Fact]
        public async Task Buy_WithCorrectId_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ItemService Buy() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);
            this.receiptService = new ReceiptService(context);
            this.cloudinaryService = new CloudinaryService(cloudinaryUtility);
            this.itemService = new ItemService(context, cloudinaryService, userService, receiptService);

            var dummyItemId = "EyePatchId";

            var dummyUser = new AuctionUser
            {
                Id = "Hugo"
            };

            context.Users.Add(dummyUser);

            bool actualResult = await this.itemService.Buy(dummyItemId, dummyUser.Id);

            Assert.True(actualResult, errorMessagePrefix);
        }

        [Fact]
        public async Task GetByName_WithNonExistingId_ShouldThrowException()
        {
            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);
            this.receiptService = new ReceiptService(context);
            this.cloudinaryService = new CloudinaryService(cloudinaryUtility);
            this.itemService = new ItemService(context, cloudinaryService, userService, receiptService);

            var itemName = "Missing";

            await Assert.ThrowsAsync<ArgumentNullException>(() => this.itemService.GetByName(itemName));
        }

        [Fact]
        public async Task GetByName_WithExistingId_ShouldReturnRightResults()
        {
            string errorMessagePrefix = "AuctionService GetByName() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);
            this.receiptService = new ReceiptService(context);
            this.cloudinaryService = new CloudinaryService(cloudinaryUtility);
            this.itemService = new ItemService(context, cloudinaryService, userService, receiptService);

            var expectedData = context.Items.First();
            var actualData = await this.itemService.GetByName(expectedData.Name);

            Assert.True(actualData.Name == expectedData.Name, errorMessagePrefix + "Names are different.");
            Assert.True(actualData.StartingPrice == expectedData.StartingPrice, errorMessagePrefix + "Starting prices are different.");
            Assert.True(actualData.BuyOutPrice == expectedData.BuyOutPrice, errorMessagePrefix + "Buy out prices are different.");
            Assert.True(actualData.StartTime == expectedData.StartTime, errorMessagePrefix + "Start times are different.");
            Assert.True(actualData.EndTime == expectedData.EndTime, errorMessagePrefix + "End times are different.");
            Assert.True(actualData.Description == expectedData.Description, errorMessagePrefix + "Descriptions are different.");
        }

        [Fact]
        public async Task GetById_WithExistingId_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "ItemService GetById() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);
            this.receiptService = new ReceiptService(context);
            this.cloudinaryService = new CloudinaryService(cloudinaryUtility);
            this.itemService = new ItemService(context, cloudinaryService, userService, receiptService);

            var expectedData = context.Items.First();
            var actualData = await this.itemService.GetById(expectedData.Id);

            Assert.True(actualData.Name == expectedData.Name, errorMessagePrefix + "Names are different.");
            Assert.True(actualData.StartingPrice == expectedData.StartingPrice, errorMessagePrefix + "Starting prices are different.");
            Assert.True(actualData.BuyOutPrice == expectedData.BuyOutPrice, errorMessagePrefix + "Buy out prices are different.");
            Assert.True(actualData.StartTime == expectedData.StartTime, errorMessagePrefix + "Start times are different.");
            Assert.True(actualData.EndTime == expectedData.EndTime, errorMessagePrefix + "End times are different.");
            Assert.True(actualData.Description == expectedData.Description, errorMessagePrefix + "Descriptions are different.");
        }

        [Fact]
        public async Task GetById_WithNonExistingId_ShouldThrowException()
        {
            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);
            this.receiptService = new ReceiptService(context);
            this.cloudinaryService = new CloudinaryService(cloudinaryUtility);
            this.itemService = new ItemService(context, cloudinaryService, userService, receiptService);

            var itemId = "Missing";

            await Assert.ThrowsAsync<ArgumentNullException>(() => this.itemService.GetById(itemId));
        }

        [Fact]
        public async Task GetAllItems_WithDummData_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ItemService GetAllItems does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);
            this.receiptService = new ReceiptService(context);
            this.cloudinaryService = new CloudinaryService(cloudinaryUtility);
            this.itemService = new ItemService(context, cloudinaryService, userService, receiptService);

            List<Item> actualResults = await
                this.itemService.GetAllItems().ToListAsync();
            List<Item> expectedResults = GetDummyData();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Name == actualEntry.Name, errorMessagePrefix + "Names are different.");
                Assert.True(expectedEntry.Description == actualEntry.Description, errorMessagePrefix + "Descriptions are different.");
                Assert.True(expectedEntry.StartingPrice == actualEntry.StartingPrice, errorMessagePrefix + "Starting prices are different.");
                Assert.True(expectedEntry.BuyOutPrice == actualEntry.BuyOutPrice, errorMessagePrefix + "Buy out prices are different.");
            }
        }

        [Fact]
        public async Task GetAllAuctionHouses_WithDummData_ShouldZeroResults()
        {
            string errorMessagePrefix = "ItemService GetAllItems does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            this.userService = new UserService(context);
            this.receiptService = new ReceiptService(context);
            this.cloudinaryService = new CloudinaryService(cloudinaryUtility);
            this.itemService = new ItemService(context, cloudinaryService, userService, receiptService);

            List<Item> actualResults = await
                this.itemService.GetAllItems().ToListAsync();

            Assert.True(actualResults.Count == 0, errorMessagePrefix);
        }

        [Fact]
        public async Task Create_WithCorrectData_ShouldReturnSuccessfullyCreate()
        {
            string errorMessagePrefix = "ItemService GetAllItems does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);
            this.receiptService = new ReceiptService(context);

            Account cloudinaryCredentials = new Account(
                "auction-cloud",
                "245777487727621",
                "VcLN0NRPB7qs0WZv_U2AxMR79sc");

            var cloudinary = new Cloudinary(cloudinaryCredentials);

            this.cloudinaryService = new CloudinaryService(cloudinary);
            this.itemService = new ItemService(context, cloudinaryService, userService, receiptService);

            var user = new AuctionUser
            {
                Id = "Hugo"
            };

            context.Users.Add(user);

            using (var stream = File.OpenRead(@"./Images/test.jpg"))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(@"./Images/test.jpg"))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg"
                };

                var dummyItem = new ItemCreateInputModel
                {
                    Name = "Stick",
                    Category = "Other",
                    Description = "Somethin",
                    AuctionDuration = 12,
                    StartingPrice = 334M,
                    BuyOutPrice = 3412M,
                    Picture = file,
                    AuctionHouse = "Ember"
                };



                bool actualResult = await this.itemService.Create(dummyItem, user.Id);
                Assert.True(actualResult, errorMessagePrefix);
            }
        }

        [Fact]
        public async Task Create_WithWrongCategory_ShouldThrowException()
        {
            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);
            this.receiptService = new ReceiptService(context);

            Account cloudinaryCredentials = new Account(
                "auction-cloud",
                "245777487727621",
                "VcLN0NRPB7qs0WZv_U2AxMR79sc");

            var cloudinary = new Cloudinary(cloudinaryCredentials);

            this.cloudinaryService = new CloudinaryService(cloudinary);
            this.itemService = new ItemService(context, cloudinaryService, userService, receiptService);

            var user = new AuctionUser
            {
                Id = "Hugo"
            };

            context.Users.Add(user);

            using (var stream = File.OpenRead(@"./Images/test.jpg"))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(@"./Images/test.jpg"))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg"
                };

                var dummyItem = new ItemCreateInputModel
                {
                    Name = "Stick",
                    Category = "Wrong",
                    Description = "Somethin",
                    AuctionDuration = 12,
                    StartingPrice = 334M,
                    BuyOutPrice = 3412M,
                    Picture = file,
                    AuctionHouse = "Ember"
                };


                await Assert.ThrowsAsync<ArgumentNullException>(() => this.itemService.Create(dummyItem, user.Id));
            }
        }

        [Fact]
        public async Task Create_WithMissingAuctionHouse_ShouldThrowException()
        {
            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.userService = new UserService(context);
            this.receiptService = new ReceiptService(context);

            Account cloudinaryCredentials = new Account(
                "auction-cloud",
                "245777487727621",
                "VcLN0NRPB7qs0WZv_U2AxMR79sc");

            var cloudinary = new Cloudinary(cloudinaryCredentials);

            this.cloudinaryService = new CloudinaryService(cloudinary);
            this.itemService = new ItemService(context, cloudinaryService, userService, receiptService);

            var user = new AuctionUser
            {
                Id = "Hugo"
            };

            context.Users.Add(user);

            using (var stream = File.OpenRead(@"./Images/test.jpg"))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(@"./Images/test.jpg"))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg"
                };

                var dummyItem = new ItemCreateInputModel
                {
                    Name = "Stick",
                    Category = "Other",
                    Description = "Somethin",
                    AuctionDuration = 12,
                    StartingPrice = 334M,
                    BuyOutPrice = 3412M,
                    Picture = file,
                    AuctionHouse = "Wrong"
                };


                await Assert.ThrowsAsync<ArgumentNullException>(() => this.itemService.Create(dummyItem, user.Id));
            }
        }
    }
}