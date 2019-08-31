using Auction.Data;
using Auction.Data.Models;
using Auction.Services.Interfaces;
using Auction.Services.Services;
using Auction.Tests.Common;
using Auction.Web.InputModels.AuctionHouse;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Auction.Tests.Services
{
    public class AuctionHouseServiceTests
    {
        private IAuctionHouseService auctionHouseService;

        private List<AuctionHouse> GetDummyData()
        {
            return new List<AuctionHouse>
            {
                new AuctionHouse
                {
                    Name = "Emporium Auction",
                    Address = "North Carolina",
                    Description = "Fine Art, Decorative Arts",
                    City = new City
                    {
                        Name = "Ashville"
                    }
                },

                new AuctionHouse
                {
                    Name = "Premier Auction",
                    Address = "Virginia",
                    Description = "World-Class Consignments and Exceptional Results",
                    City = new City
                    {
                        Name = "Richmond"
                    }
                }
        };

        }

        private async Task SeedData(AuctionDbContext context)
        {
            context.AddRange(GetDummyData());
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetAllAuctionHouses_WithDummData_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "AuctionService GetAllAuctionHouses does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.auctionHouseService = new AuctionHouseService(context);

            List<AuctionHouse> actualResults = await
                this.auctionHouseService.GetAllAuctionHouses().ToListAsync();
            List<AuctionHouse> expectedResults = GetDummyData();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Address == actualEntry.Address, errorMessagePrefix + "Adresses are different.");
                Assert.True(expectedEntry.Description == actualEntry.Description, errorMessagePrefix + "Descriptions are different.");
                Assert.True(expectedEntry.City.Name == actualEntry.City.Name, errorMessagePrefix + "City names are different.");

            }
        }

        [Fact]
        public async Task GetAllAuctionHouses_WithDummData_ShouldZeroResults()
        {
            string errorMessagePrefix = "AuctionService GetAllAuctionHouses does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            this.auctionHouseService = new AuctionHouseService(context);

            List<AuctionHouse> actualResults = await
                this.auctionHouseService.GetAllAuctionHouses().ToListAsync();

            Assert.True(actualResults.Count == 0, errorMessagePrefix);
        }

        [Fact]
        public async Task GetById_WithExistingId_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "AuctionService GetById() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.auctionHouseService = new AuctionHouseService(context);

            var expectedData = context.AuctionHouses.First();
            var actualData = await this.auctionHouseService.GetById(expectedData.Id);

            Assert.True(expectedData.Id == actualData.Id, errorMessagePrefix + "Ids are different.");
            Assert.True(expectedData.Address == actualData.Address, errorMessagePrefix + "Adresses are different.");
            Assert.True(expectedData.Description == actualData.Description, errorMessagePrefix + "Descriptions are different.");
            Assert.True(expectedData.City.Name == actualData.City.Name, errorMessagePrefix + "City names are different.");
        }

        [Fact]
        public async Task GetById_WithNonExistingId_ShouldReturnNull()
        {
            string errorMessagePrefix = "AuctionService GetById() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.auctionHouseService = new AuctionHouseService(context);

            var actualData = await this.auctionHouseService.GetById("Missing");

            Assert.True(actualData == null, errorMessagePrefix);
        }

        [Fact]
        public async Task Create_WithCorrectData_ShouldReturnSuccessfullyCreate()
        {
            string errorMessagePrefix = "AuctionService Create() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.auctionHouseService = new AuctionHouseService(context);

            var testAuctionHouse = new AuctionHouseCreateInputModel
            {
                Name = "Lucky",
                Address = "Hope street",
                Description = "We belive",
                City = "Richmond"
            };

            bool actualResult = await this.auctionHouseService.Create(testAuctionHouse);

            Assert.True(actualResult, errorMessagePrefix);
        }

        [Fact]
        public async Task Create_WithNotExistingCity_ShouldReturnSuccessfullyCreate()
        {
            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.auctionHouseService = new AuctionHouseService(context);

            var testAuctionHouse = new AuctionHouseCreateInputModel
            {
                Name = "Lucky",
                Address = "Hope street",
                Description = "We belive",
                City = "Yeet"
            };

            await Assert.ThrowsAsync<ArgumentNullException>(() => this.auctionHouseService.Create(testAuctionHouse));
        }

        [Fact]
        public async Task GetByName_WithExistingName_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "AuctionService GetByName() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.auctionHouseService = new AuctionHouseService(context);

            var expectedData = context.AuctionHouses.First();
            var actualData = await this.auctionHouseService.GetByName(expectedData.Name);

            Assert.True(expectedData.Id == actualData.Id, errorMessagePrefix + "Ids are different.");
            Assert.True(expectedData.Address == actualData.Address, errorMessagePrefix + "Adresses are different.");
            Assert.True(expectedData.Description == actualData.Description, errorMessagePrefix + "Descriptions are different.");
            Assert.True(expectedData.City.Name == actualData.City.Name, errorMessagePrefix + "City names are different.");
        }

        [Fact]
        public async Task GetByName_WithNonExistingId_ShouldReturnNull()
        {
            string errorMessagePrefix = "AuctionService GetByName() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.auctionHouseService = new AuctionHouseService(context);

            var actualData = await this.auctionHouseService.GetByName("Missing");

            Assert.True(actualData == null, errorMessagePrefix);
        }

        [Fact]
        public async Task CreateReview_WithCorrectData_ShouldReturnSuccessfullyCreate()
        {
            string errorMessagePrefix = "AuctionService CreateReview() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.auctionHouseService = new AuctionHouseService(context);

            var dummyAuctionId = context.AuctionHouses.First().Id;

            var reviewModel = new AuctionHouseReviewInputModel
            {
                Author = "Hugo",
                Description = "Hugo is the best"
            };

            bool actualResult = await this.auctionHouseService.CreateReview(dummyAuctionId, reviewModel);

            Assert.True(actualResult, errorMessagePrefix);
        }
    }
}
