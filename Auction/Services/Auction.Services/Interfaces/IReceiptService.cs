using System.Threading.Tasks;

namespace Auction.Services.Interfaces
{
    public interface IReceiptService
    {
        Task<bool> CreateReceipt(string itemId, string ownerId);
    }
}
