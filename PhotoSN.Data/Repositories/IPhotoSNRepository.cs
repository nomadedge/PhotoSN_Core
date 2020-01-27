using PhotoSN.Model.Dtos;
using PhotoSN.Model.IdentityInputModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoSN.Data.Repositories
{
    public interface IPhotoSNRepository
    {
        Task<List<AvatarsHistoryInputModel>> GetAvatarsAsync(int userId);
        Task<int?> GetCurrentAvatarAsync(int userId);
        Task CreateAvatarAsync(AvatarDto createAvatarDto);
        Task DeleteAvatarAsync(AvatarDto avatarDto);
        Task ChangeCurrentAvatarAsync(AvatarDto avatarDto);
        Task<int> CreateImageAsync(CreateImageDto createImageDto);
        Task<GetImageDto> GetImageAsync(int imageId);
        Task DeleteImageAsync(int imageId);
    }
}