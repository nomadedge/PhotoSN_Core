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
            return Path.Combine(Directory.GetCurrentDirectory(), _imageHelperOptions.BlobStoragePath, guid.ToString("N"));
        }

        public async Task SaveImageAsync(byte[] byteArray, Guid guid)
        {
            var fileName = GetFullFileName(guid);
            await File.WriteAllBytesAsync(fileName, byteArray);
        }

        public async Task<byte[]> ReadImageAsync(Guid guid)
        {
            var fileName = GetFullFileName(guid);
            var fileInfo = new FileInfo(fileName);
            if (fileInfo != null && fileInfo.Exists)
            {
                return await File.ReadAllBytesAsync(fileInfo.FullName);
            }
            throw new FileNotFoundException($"{fileInfo} not found.");
        }
    }
}
