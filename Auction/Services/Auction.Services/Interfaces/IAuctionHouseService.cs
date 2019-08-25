using Auction.Data.Models;
using Auction.Web.InputModels.AuctionHouse;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Services.Interfaces
{
    public interface IAuctionHouseService
    {
        Task<bool> Create(AuctionHouseCreateInputModel inputModel);

        IQueryable<AuctionHouse> GetAllAuctionHouses();

        Task<AuctionHouse> GetById(string id);
    }
}
