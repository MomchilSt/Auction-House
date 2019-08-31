using Auction.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Auction.Tests.Common
{
    public static class AuctionDbContextInMemory
    {
        public static AuctionDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<AuctionDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            return new AuctionDbContext(options);
        }
    }
}
