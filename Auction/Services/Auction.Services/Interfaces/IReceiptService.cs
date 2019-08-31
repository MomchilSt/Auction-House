using Auction.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Services.Interfaces
{
    public interface IReceiptService
    {
        Task<bool> CreateReceipt(string itemId, string ownerId);

        IQueryable<Receipt> GetReceiptsById(string id);
    }
}
