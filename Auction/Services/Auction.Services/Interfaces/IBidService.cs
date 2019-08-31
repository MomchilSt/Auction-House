using Auction.Data.Models;
using System.Threading.Tasks;

namespace Auction.Services.Interfaces
{
    public interface IBidService
    {
        Task<bool> CreateBid(Item item, string bidderId, decimal amountParsed);
    }
}
