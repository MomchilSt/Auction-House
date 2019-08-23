using Auction.Web.InputModels.Item;
using System.Threading.Tasks;

namespace Auction.Services.Interfaces
{
    public interface IItemService
    {
        Task<bool> Create(ItemCreateInputModel inputModel);
    }
}
