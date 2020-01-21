using PhotoSN.Model.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoSN.Data.Repositories
{
    public interface IPhotoSNRepository
    {
        Task<List<int>> GetAvatarsAsync(int userId);
        Task<int?> GetCurrentAvatarAsync(int userId);
        Task CreateAvatarAsync(AvatarDto createAvatarDto);
        Task DeleteAvatarAsync(AvatarDto avatarDto);
        Task<int> CreateImageAsync(CreateImageDto createImageDto);
        Task<GetImageDto> GetImageAsync(int imageId);
        Task DeleteImageAsync(int imageId);
    }
}