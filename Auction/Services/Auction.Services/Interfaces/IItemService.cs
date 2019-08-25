using Auction.Data.Models;
using Auction.Web.InputModels.Item;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Services.Interfaces
{
    public interface IItemService
    {
        Task<bool> Create(ItemCreateInputModel inputModel);

        IQueryable<Item> GetAllItems();

        Task<Item> GetById(string id);

        Task<bool> Delete(string id);
    }
}
