using Auction.Data.Models;
using Auction.Web.InputModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Services.Interfaces
{
    public interface ICityService
    {
        Task<bool> CreateCity(CityCreateInputModel inputModel);

        IQueryable<City> GetCities();
    }
}
