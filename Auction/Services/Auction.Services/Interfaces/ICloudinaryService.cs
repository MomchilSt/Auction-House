using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Auction.Services.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadPictureAsync(IFormFile pictureFile, string fileName);
    }
}
