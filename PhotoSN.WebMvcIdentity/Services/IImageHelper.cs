using System;
using System.Threading.Tasks;

namespace PhotoSN.WebMvcIdentity.Services
{
    public interface IImageHelper
    {
        Task<byte[]> ReadImageAsync(Guid guid);
        Task SaveImageAsync(byte[] byteArray, Guid guid);
    }
}