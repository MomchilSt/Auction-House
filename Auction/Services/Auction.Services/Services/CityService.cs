using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auction.Data;
using Auction.Data.Models;
using Auction.Services.Interfaces;
using Auction.Web.InputModels;
using Microsoft.EntityFrameworkCore;

namespace Auction.Services.Services
{
    public class CityService : ICityService
    {
        private readonly AuctionDbContext context;

        public CityService(AuctionDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateCity(CityCreateInputModel inputModel)
        {
            City city = new City
            {
                Name = inputModel.Name
            };

            context.Cities.Add(city);
            int result = await context.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<City> GetCities()
        {
            var cities = this.context.Cities;

            return cities;
        }
    }
}
