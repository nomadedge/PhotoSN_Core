using PhotoSN.Data.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoSN.Data.Repositories
{
    public interface IPhotoSNRepository
    {
        Task<List<AvatarsHistoryDto>> GetAvatarsAsync(int userId);
        Task<int?> GetCurrentAvatarAsync(int userId);
        Task CreateAvatarAsync(AvatarDto createAvatarDto);
        Task DeleteAvatarAsync(AvatarDto avatarDto);
        Task ChangeCurrentAvatarAsync(AvatarDto avatarDto);
        Task<int> CreateImageAsync(CreateImageDto createImageDto);
        Task<GetImageDto> GetImageAsync(int imageId);
        Task DeleteImageAsync(int imageId);
        Task<int> CreatePostAsync(CreatePostDto createPostDto);
        Task<GetFullPostDto> GetFullPostAsync(int postId);
        Task<List<GetPostDto>> GetPostsByUserIdAsync(int userId, bool isAuthorized, int currentUserId, int postId);
        Task<List<GetPostDto>> GetFeedByUserIdAsync(int userId, int postId);
        Task<GetUserDto> GetUserAsync(int userId);
        Task FollowToUserAsync(int followerUserId, int followingUserId);
        Task UnfollowFromUserAsync(int followerUserId, int followingUserId);
        Task<List<GetSimpleUserDto>> GetFollowersAsync(int userId);
        Task<List<GetSimpleUserDto>> GetFollowingsAsync(int userId);
        Task<bool> LikeOrDislikePostAsync(int userId, int postId);
        Task<List<GetCommentDto>> GetCommentsByPostIdAsync(int postId, bool isAuthorized, int currentUserId, int commentId);
        Task<bool> LikeOrDislikeCommentAsync(int userId, int commentId);
        Task<GetCommentDto> CreateCommentAsync(CreateCommentDto createCommentDto);
    }
}