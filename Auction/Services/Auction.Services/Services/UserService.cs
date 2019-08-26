using System.Linq;
using System.Threading.Tasks;
using Auction.Data;
using Auction.Data.Models;
using Auction.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Auction.Services.Services
{
    public class UserService : IUserService
    {
        private readonly AuctionDbContext context;

        public UserService(AuctionDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Edit(string id, string fullName)
        {
            var userFromDb = await this.context.Users
                .SingleOrDefaultAsync(user => user.Id == id);

            userFromDb.FullName = fullName;

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<AuctionUser> GetById(string id)
        {
            return await this.context.Users.SingleOrDefaultAsync(user => user.Id == id);
        }
    }
}
