using Auction.Data;
using Auction.Data.Models;
using Auction.Services.Interfaces;
using Auction.Services.Services;
using Auction.Tests.Common;
using Auction.Web.InputModels.City;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Auction.Tests.Services
{
    public class CityServiceTests
    {
        private ICityService cityService;

        private List<City> GetDummyData()
        {
            return new List<City>
            {
                new City
                {
                    Name = "Ashville"
                },

                new City
                {
                    Name = "Richmond"
                }};

        }

        private async Task SeedData(AuctionDbContext context)
        {
            context.AddRange(GetDummyData());
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetAllCities_WithDummData_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "CityService GetAllCities() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.cityService = new CityService(context);

            List<City> actualResults = await
                this.cityService.GetAllCities().ToListAsync();
            List<City> expectedResults = GetDummyData();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Name == actualEntry.Name, errorMessagePrefix + "City names are different.");
            }
        }

        [Fact]
        public async Task GetAllAuctionHouses_WithDummData_ShouldZeroResults()
        {
            string errorMessagePrefix = "CityService GetAllCities does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            this.cityService = new CityService(context);

            List<City> actualResults = await
                this.cityService.GetAllCities().ToListAsync();

            Assert.True(actualResults.Count == 0, errorMessagePrefix);
        }

        [Fact]
        public async Task Create_WithCorrectData_ShouldReturnSuccessfullyCreate()
        {
            string errorMessagePrefix = "CityService CreateCity() does not work properly.";

            var context = AuctionDbContextInMemory.InitializeContext();
            await SeedData(context);
            this.cityService = new CityService(context);

            var testAuctionHouse = new CityCreateInputModel
            {
                Name = "Sofia"
            };

            bool actualResult = await this.cityService.CreateCity(testAuctionHouse);

            Assert.True(actualResult, errorMessagePrefix);
        }
    }
}
