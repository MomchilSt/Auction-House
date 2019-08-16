using Auction.Web.InputModels;
using System.Threading.Tasks;

namespace Auction.Services.Interfaces
{
    public interface ICityService
    {
        Task<bool> CreateCity(CityCreateInputModel inputModel);
    }
}
