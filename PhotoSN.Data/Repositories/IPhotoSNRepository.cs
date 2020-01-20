using PhotoSN.Model.Dtos;
using System.Threading.Tasks;

namespace PhotoSN.Data.Repositories
{
    public interface IPhotoSNRepository
    {
        Task<int> CreateImageAsync(CreateImageDto createImageDto);
        Task<GetImageDto> GetImageAsync(int imageId);
    }
}