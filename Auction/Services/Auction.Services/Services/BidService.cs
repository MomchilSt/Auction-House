using Auction.Data;
using Auction.Data.Models;
using Auction.Data.Models.Enums;
using Auction.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Services.Services
{
    public class BidService : IBidService
    {
        private readonly AuctionDbContext context;
        private readonly IUserService userService;

        public BidService(AuctionDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<bool> CreateBid(Item item, string bidderId, decimal amountParsed)
        {
            var userFromDb = await this.userService.GetById(bidderId);

            if (userService == null ||
                item == null ||
                item.EndTime < DateTime.UtcNow ||
                amountParsed < item.StartingPrice)
            {
                return false;
            }

            var bidWithHighestAmmount = item
                .Bids
                .OrderByDescending(x => x.Amount)
                .FirstOrDefault();

            if (bidWithHighestAmmount == null || bidWithHighestAmmount.Amount < amountParsed)
            {
                var bid = new Bid
                {
                    CreatedOn = DateTime.UtcNow,
                    Amount = amountParsed,
                    ItemId = item.Id,
                    UserId = bidderId
                };

                await this.context.Bids.AddAsync(bid);
                item.Status = ItemStatus.BidedOn;

                int result = await this.context
                .SaveChangesAsync();

                return result > 0;
            }

            return false;
        }
    }
}