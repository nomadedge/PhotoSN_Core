using PhotoSN.Model.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoSN.Data.Repositories
{
    public interface IPhotoSNRepository
    {
        List<int> GetAvatars(int userId);
        Task CreateAvatarAsync(CreateAvatarDto createAvatarDto);
        Task<int> CreateImageAsync(CreateImageDto createImageDto);
        Task<GetImageDto> GetImageAsync(int imageId);
    }
}