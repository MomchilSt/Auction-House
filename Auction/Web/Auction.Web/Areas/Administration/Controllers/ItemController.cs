using Auction.Services.Interfaces;
using Auction.Web.InputModels.Item;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Auction.Web.Areas.Administration.Controllers
{
    public class ItemController : AdminController
    {
        private readonly IItemService itemService;

        public ItemController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            await this.itemService.Delete(id);

            return this.Redirect("/");
        }

        [HttpPost(Name = "Delete")]
        public async Task<IActionResult> Delete(ItemDeletInputModel inputModel)
        {
            await this.itemService.Delete(inputModel.ItemId);

            return RedirectToHome();
        }
    }
}
