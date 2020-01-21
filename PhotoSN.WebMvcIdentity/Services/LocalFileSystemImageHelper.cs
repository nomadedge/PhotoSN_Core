using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PhotoSN.WebMvcIdentity.Services
{
    public class LocalFileSystemImageHelper : IImageHelper
    {
        private ImageHelperOptions _imageHelperOptions;

        public LocalFileSystemImageHelper(
            IOptions<ImageHelperOptions> optionsAccessor)
        {
            _imageHelperOptions = optionsAccessor.Value;
        }

        private string GetFullFileName(Guid guid)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), _imageHelperOptions.ImageStoragePath, guid.ToString());
        }

        public async Task SaveImageAsync(IFormFile image, Guid guid)
        {
            var fileName = GetFullFileName(guid);
            var fileStream = new FileStream(fileName, FileMode.Create);
            await image.CopyToAsync(fileStream);
            fileStream.Close();
        }

        public async Task<byte[]> ReadImageAsync(Guid guid)
        {
            var fileName = GetFullFileName(guid);
            var fileInfo = new FileInfo(fileName);
            if (fileInfo != null && fileInfo.Exists)
            {
                return await File.ReadAllBytesAsync(fileInfo.FullName);
            }
            throw new FileNotFoundException($"{fileName} not found.");
        }
    }
}
