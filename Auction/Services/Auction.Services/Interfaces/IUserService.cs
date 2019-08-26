using Auction.Data.Models;
using System.Threading.Tasks;

namespace Auction.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuctionUser> GetById(string id);

        Task<bool> Edit(string id, string fullName);
    }
}
