using Auction.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auction.Data
{
    public class AuctionDbContext : IdentityDbContext<AuctionUser, IdentityRole, string>
    {
        public DbSet<AuctionHouse> AuctionHouses { get; set; }

        public DbSet<Bid> Bids { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public AuctionDbContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}
