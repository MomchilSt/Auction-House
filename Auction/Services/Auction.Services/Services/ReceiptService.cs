using System;
using System.Linq;
using System.Threading.Tasks;
using Auction.Data;
using Auction.Data.Models;
using Auction.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Auction.Services.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly AuctionDbContext context;

        public ReceiptService(AuctionDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateReceipt(string itemId, string ownerId)
        {
            var itemFromDb = context
                .Items
                .FirstOrDefault(x => x.Id == itemId);

            if (itemFromDb == null)
            {
                throw new ArgumentNullException(nameof(itemFromDb));
            }

            var receipt = new Receipt
            {
                IssuedOn = DateTime.UtcNow,
                ItemId = itemId,
                UserId = ownerId
            };

            await this.context.Receipts.AddAsync(receipt);

            int result = await this.context
                .SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<Receipt> GetReceiptsById(string id)
        {
            var receipts = this.context.Receipts
                .Include(x => x.Item)
                .Where(x => x.UserId == id);

            return receipts;
        }
    }
}
