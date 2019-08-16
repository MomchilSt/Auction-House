using Auction.Web.InputModels;
using System.Threading.Tasks;

namespace Auction.Services.Interfaces
{
    public interface IAuctionHouseService
    {
        Task<bool> Create(AuctionHouseCreateInputModel inputModel);
    }
}
