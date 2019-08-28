using System;
using System.Threading.Tasks;
using Auction.Data;
using Auction.Data.Models;
using Auction.Services.Interfaces;

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
    }
}
