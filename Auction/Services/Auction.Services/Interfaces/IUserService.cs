using Auction.Data.Models;
using Auction.Web.InputModels.Item;
using Auction.Web.InputModels.User;
using System.Threading.Tasks;

namespace Auction.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuctionUser> GetById(string id);

        Task<bool> Edit(string id, string fullName);

        Task<bool> Delete(string id, string name);
    }
}
