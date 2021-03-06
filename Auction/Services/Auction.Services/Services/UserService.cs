﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Auction.Data;
using Auction.Data.Models;
using Auction.Data.Models.Enums;
using Auction.Services.Interfaces;
using Auction.Web.InputModels.Item;
using Auction.Web.InputModels.User;
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

        public async Task<bool> Delete(string id, string name)
        {
            var userFromDb = await this.context.Users.SingleOrDefaultAsync(x => x.Id == id);
            var item = userFromDb.ItemsAuctioned.SingleOrDefault(x => x.Name == name);

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            userFromDb.ItemsAuctioned.Remove(item);

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Edit(string id, string fullName)
        {
            var userFromDb = await this.context
                .Users
                .SingleOrDefaultAsync(user => user.Id == id);

            userFromDb.FullName = fullName;

            int result = await this.context
                .SaveChangesAsync();

            return result > 0;
        }
 
        public async Task<AuctionUser> GetById(string id)
        {
            return await this.context.Users
                .Include(x => x.ItemsAuctioned)
                .SingleOrDefaultAsync(user => user.Id == id);
        }       
    }
}
