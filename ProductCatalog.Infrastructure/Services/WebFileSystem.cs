using Microsoft.AspNetCore.Http;
using ProductCatalog.Core.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProductCatalog.Infrastructure.Services
{
    public class WebFileSystem : IFileSystem
    {
        private readonly string _mediaPath;

        public WebFileSystem(string mediaPath)
        {
            _mediaPath = mediaPath;
        }

        public async Task<string> SavePicture(IFormFile formFile)
        {
            Guid documentGuid = Guid.NewGuid();

            if (formFile.Length > 0)
            {
                var fileName = documentGuid.ToString() + formFile.FileName;
                
                var fullPath = Path.Combine(_mediaPath, fileName);

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                return fileName;
            }
            else
            {
                throw new FileNotFoundException(formFile.FileName);
            }
        }

        public async Task<string> ReplacePicture(IFormFile formFile, string fileNameToReplace)
        {
            DeletePicture(fileNameToReplace);
            return await SavePicture(formFile);
        }

        public void DeletePicture(string fileName)
        {
            var fullPath = Path.Combine(_mediaPath, fileName);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException(fileName);
            
            File.Delete(fullPath);
        }
    }
}
