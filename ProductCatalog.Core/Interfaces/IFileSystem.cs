using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ProductCatalog.Core.Interfaces
{
    public interface IFileSystem
    {
        Task<string> SavePicture(IFormFile formFile);
        Task<string> ReplacePicture(IFormFile formFile, string fileNameToReplace);
        void DeletePicture(string fileName);
    }
}
