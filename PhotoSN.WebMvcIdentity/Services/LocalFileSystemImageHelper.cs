using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
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

        public async Task SaveAvatarAsync(IFormFile image, Guid guid)
        {
            var tempFileName = GetFullFileName(guid) + "Temp";
            var constFileName = GetFullFileName(guid);
            var fileStream = new FileStream(tempFileName, FileMode.Create);
            await image.CopyToAsync(fileStream);
            fileStream.Close();

            using (var tempFileStream = File.OpenRead(tempFileName))
            {
                using (var constFileStream = File.OpenWrite(constFileName))
                {
                    using (var avatar = Image.Load(tempFileStream))
                    {
                        avatar.Mutate(ipc => ipc.Resize(new ResizeOptions
                        {
                            Mode = ResizeMode.Crop,
                            Size = new Size(300)
                        }));

                        avatar.SaveAsPng(constFileStream);
                    }
                }
            }

            File.Delete(tempFileName);
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

        public void DeleteImage(Guid guid)
        {
            var fileName = GetFullFileName(guid);
            var fileInfo = new FileInfo(fileName);
            if (fileInfo == null || !fileInfo.Exists)
            {
                throw new FileNotFoundException($"{fileName} not found.");
            }
            File.Delete(fileInfo.FullName);
        }
    }
}
