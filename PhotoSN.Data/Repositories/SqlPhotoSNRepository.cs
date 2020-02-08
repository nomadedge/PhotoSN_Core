﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoSN.Data.DbContexts;
using PhotoSN.Data.Dtos;
using PhotoSN.Data.Entities;
using PhotoSN.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoSN.Data.Repositories
{
    public class SqlPhotoSNRepository : IPhotoSNRepository
    {
        private readonly PhotoSNDbContext _photoSNDbContext;
        private readonly IMapper _mapper;

        public SqlPhotoSNRepository(
            PhotoSNDbContext photoSNDbContext,
            IMapper mapper)
        {
            _photoSNDbContext = photoSNDbContext;
            _mapper = mapper;
        }

        public async Task<List<AvatarsHistoryDto>> GetAvatarsAsync(int userId)
        {
            var avatars = await _photoSNDbContext.Avatars
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<AvatarsHistoryDto>>(avatars);
        }

        public async Task<int?> GetCurrentAvatarAsync(int userId)
        {
            var avatar = await _photoSNDbContext.Avatars
                .FirstOrDefaultAsync(a => (a.UserId == userId && a.IsCurrent == true));
            if (avatar != null)
            {
                return avatar.ImageId;
            }
            return null;
        }

        public async Task CreateAvatarAsync(AvatarDto avatarDto)
        {
            using (var transaction = _photoSNDbContext.Database.BeginTransaction())
            {
                try
                {
                    var currentAvatar = await _photoSNDbContext.Avatars
                        .FirstOrDefaultAsync(a => (a.UserId == avatarDto.UserId && a.IsCurrent == true));
                    if (currentAvatar != null)
                    {
                        currentAvatar.IsCurrent = false;
                    }

                    var newAvatar = _mapper.Map<Avatar>(avatarDto);
                    newAvatar.IsCurrent = true;

                    await _photoSNDbContext.Avatars.AddAsync(newAvatar);

                    await _photoSNDbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        public async Task DeleteAvatarAsync(AvatarDto avatarDto)
        {
            using (var transaction = _photoSNDbContext.Database.BeginTransaction())
            {
                try
                {
                    var avatar = await _photoSNDbContext.Avatars
                        .FirstOrDefaultAsync(a => (a.UserId == avatarDto.UserId && a.ImageId == avatarDto.ImageId));
                    if (avatar == null)
                    {
                        throw new ArgumentException("Avatar not found.");
                    }

                    if (avatar.IsCurrent == true)
                    {
                        var avatars = _photoSNDbContext.Avatars
                            .Where(a => a.UserId == avatarDto.UserId)
                            .ToList();
                        if (avatars.Count != 1)
                        {
                            if (avatars.Last().IsCurrent != true)
                            {
                                avatars.Last().IsCurrent = true;
                            }
                            else
                            {
                                avatars[avatars.Count - 2].IsCurrent = true;
                            }
                        }
                    }

                    _photoSNDbContext.Avatars.Remove(avatar);

                    await _photoSNDbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        public async Task ChangeCurrentAvatarAsync(AvatarDto avatarDto)
        {
            using (var transaction = _photoSNDbContext.Database.BeginTransaction())
            {
                try
                {
                    var avatar = await _photoSNDbContext.Avatars
                        .FirstOrDefaultAsync(a => (a.UserId == avatarDto.UserId && a.ImageId == avatarDto.ImageId));
                    if (avatar == null)
                    {
                        throw new ArgumentException("Avatar not found.");
                    }
                    if (avatar.IsCurrent)
                    {
                        throw new InvalidOperationException("Avatar is already current.");
                    }

                    var currentAvatar = await _photoSNDbContext.Avatars
                        .FirstOrDefaultAsync(a => (a.UserId == avatarDto.UserId && a.IsCurrent == true));
                    if (avatar != null)
                    {
                        currentAvatar.IsCurrent = false;
                    }

                    avatar.IsCurrent = true;

                    await _photoSNDbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        public async Task<int> CreateImageAsync(CreateImageDto createImageDto)
        {
            if (!createImageDto.MimeType.Contains("image"))
            {
                throw new ArgumentException("File type should be image");
            }
            using (var transaction = _photoSNDbContext.Database.BeginTransaction())
            {
                try
                {
                    var newImage = _mapper.Map<Image>(createImageDto);
                    newImage.User = await _photoSNDbContext.Users.FindAsync(createImageDto.UserId);
                    newImage.Created = DateTime.Now;

                    await _photoSNDbContext.Images.AddAsync(newImage);

                    await _photoSNDbContext.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return newImage.ImageId;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        public async Task<GetImageDto> GetImageAsync(int imageId)
        {
            try
            {
                var image = await _photoSNDbContext.Images
                    .Include(i => i.User)
                    .FirstOrDefaultAsync(i => i.ImageId == imageId);
                if (image == null)
                {
                    throw new ArgumentException("Image not found.");
                }
                var getImageDto = _mapper.Map<GetImageDto>(image);
                return getImageDto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteImageAsync(int imageId)
        {
            using (var transaction = _photoSNDbContext.Database.BeginTransaction())
            {
                try
                {
                    var image = await _photoSNDbContext.Images
                        .FindAsync(imageId);
                    if (image == null)
                    {
                        throw new ArgumentException("Image not found.");
                    }
                    _photoSNDbContext.Images.Remove(image);

                    await _photoSNDbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        public async Task<int> CreatePostAsync(CreatePostDto createPostDto)
        {
            if (createPostDto == null ||
                createPostDto.ImageIds == null ||
                !createPostDto.ImageIds.Any() ||
                createPostDto.ImageIds.Count > 10)
            {
                throw new ArgumentOutOfRangeException("Post must contain from 1 to 10 pictures.");
            }

            using (var transaction = _photoSNDbContext.Database.BeginTransaction())
            {
                try
                {
                    var user = await _photoSNDbContext.Users
                        .FirstOrDefaultAsync(u => u.Id == createPostDto.UserId);
                    if (user == null)
                    {
                        throw new ArgumentException($"User with id {createPostDto.UserId} does not exist.");
                    }

                    var images = await _photoSNDbContext.Images
                        .Include(i => i.User)
                        .Where(i => createPostDto.ImageIds.Contains(i.ImageId) && i.User.Id == createPostDto.UserId)
                        .ToListAsync();
                    if (images.Count != createPostDto.ImageIds.Count)
                    {
                        throw new ArgumentException("All images must be presented in DB and be uploaded by the same user who is creating the post.");
                    }

                    var post = new Post
                    {
                        User = user,
                        Description = createPostDto.Description,
                        Created = DateTime.Now
                    };
                    await _photoSNDbContext.Posts.AddAsync(post);

                    byte orderNumber = 1;
                    foreach (var image in images)
                    {
                        var postImage = new PostImage
                        {
                            Image = image,
                            OrderNumber = orderNumber,
                            Post = post
                        };
                        await _photoSNDbContext.PostImages.AddAsync(postImage);
                        orderNumber++;
                    }

                    var hashtags = HashtagService.GetHashtags(createPostDto.Description);
                    foreach (var hashtag in hashtags)
                    {
                        var existingHashtag = await _photoSNDbContext.Hashtags
                            .FirstOrDefaultAsync(h => h.Text == hashtag);
                        var inPostHashtag = new InPostHashtag();
                        if (existingHashtag == null)
                        {
                            var newHashtag = new Hashtag { Text = hashtag };
                            _photoSNDbContext.Hashtags.Add(newHashtag);
                            inPostHashtag.Hashtag = newHashtag;
                        }
                        else
                        {
                            inPostHashtag.Hashtag = existingHashtag;
                        }
                        inPostHashtag.Post = post;
                        _photoSNDbContext.InPostHashtags.Add(inPostHashtag);
                    }

                    await _photoSNDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return post.PostId;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        public async Task<GetPostDto> GetPostAsync(int postId)
        {
            try
            {
                var post = await _photoSNDbContext.Posts
                    .Include(p => p.User)
                        .ThenInclude(u => u.Avatars)
                    .Include(p => p.PostImages)
                    .Include(p => p.PostLikes)
                    .Include(p => p.Comments)
                    .FirstOrDefaultAsync(p => p.PostId == postId);
                if (post == null)
                {
                    throw new ArgumentException("Post not found.");
                }

                var getPostDto = _mapper.Map<GetPostDto>(post);

                return getPostDto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<GetFullPostDto> GetFullPostAsync(int postId)
        {
            try
            {
                var post = await _photoSNDbContext.Posts
                    .Include(p => p.User)
                        .ThenInclude(u => u.Avatars)
                    .Include(p => p.PostImages)
                    .Include(p => p.PostLikes)
                        .ThenInclude(pl => pl.User)
                    .Include(p => p.Comments)
                        .ThenInclude(c => c.User)
                    .FirstOrDefaultAsync(p => p.PostId == postId);
                if (post == null)
                {
                    throw new ArgumentException("Post not found.");
                }

                var getFullPostDto = _mapper.Map<GetFullPostDto>(post);

                return getFullPostDto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<int>> GetPostsByUserIdAsync(int userId, int? postId = int.MaxValue)
        {
            try
            {
                var user = await _photoSNDbContext.Users
                    .FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    throw new ArgumentException($"User with id {userId} does not exist.");
                }

                var postIds = await _photoSNDbContext.Posts
                    .Include(p => p.User)
                    .Where(p => p.User == user && p.PostId < postId)
                    .Reverse()
                    .Take(10)
                    .Select(p => p.PostId)
                    .ToListAsync();

                return postIds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<int>> GetFeedByUserIdAsync(int userId, int? postId)
        {
            try
            {
                var user = await _photoSNDbContext.Users
                    .Include(u => u.Following)
                    .FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    throw new ArgumentException($"User with id {userId} does not exist.");
                }

                var feedIds = user.Following.Select(s => s.SecondUserId).ToList();

                var postIds = await _photoSNDbContext.Posts
                    .Include(p => p.User)
                    .Where(p => feedIds.Contains(p.User.Id) && p.PostId < postId)
                    .Reverse()
                    .Take(10)
                    .Select(p => p.PostId)
                    .ToListAsync();

                return postIds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<GetUserDto> GetUserAsync(int userId)
        {
            try
            {
                var user = await _photoSNDbContext.Users
                    .Include(u => u.Avatars)
                    .Include(u => u.Followers)
                        .ThenInclude(s => s.FirstUser)
                            .ThenInclude(u => u.Avatars)
                    .Include(u => u.Following)
                        .ThenInclude(s => s.SecondUser)
                            .ThenInclude(u => u.Avatars)
                    .FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    throw new ArgumentException($"User with id { userId } does not exist.");
                }

                var getUserDto = _mapper.Map<GetUserDto>(user);
                getUserDto.Followers = _mapper.Map<List<GetSimpleUserDto>>(user.Followers.Select(s => s.FirstUser));
                getUserDto.Following = _mapper.Map<List<GetSimpleUserDto>>(user.Following.Select(s => s.SecondUser));

                return getUserDto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task FollowToUserAsync(int followerUserId, int followingUserId)
        {
            using (var transaction = _photoSNDbContext.Database.BeginTransaction())
            {
                try
                {
                    var followerUser = await _photoSNDbContext.Users.FindAsync(followerUserId);
                    if (followerUser == null)
                    {
                        throw new ArgumentException($"User with id {followerUserId} does not exist.");
                    }
                    var followingUser = await _photoSNDbContext.Users.FindAsync(followingUserId);
                    if (followingUser == null)
                    {
                        throw new ArgumentException($"User with id {followingUserId} does not exist.");
                    }

                    var existingSubscription = await _photoSNDbContext.Subscriptions
                        .FirstOrDefaultAsync(s => s.FirstUserId == followerUserId && s.SecondUserId == followingUserId);
                    if (existingSubscription != null)
                    {
                        throw new InvalidOperationException($"User with id {followerUserId} is already following user with id {followingUserId}.");
                    }

                    var newSubscription = new Subscription
                    {
                        FirstUser = followerUser,
                        SecondUser = followingUser,
                        IsApproved = true
                    };
                    _photoSNDbContext.Subscriptions.Add(newSubscription);

                    await _photoSNDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        public async Task UnfollowFromUserAsync(int followerUserId, int followingUserId)
        {
            using (var transaction = _photoSNDbContext.Database.BeginTransaction())
            {
                try
                {
                    var followerUser = await _photoSNDbContext.Users.FindAsync(followerUserId);
                    if (followerUser == null)
                    {
                        throw new ArgumentException($"User with id {followerUserId} does not exist.");
                    }
                    var followingUser = await _photoSNDbContext.Users.FindAsync(followingUserId);
                    if (followingUser == null)
                    {
                        throw new ArgumentException($"User with id {followingUserId} does not exist.");
                    }

                    var existingSubscription = await _photoSNDbContext.Subscriptions
                        .FirstOrDefaultAsync(s => s.FirstUserId == followerUserId && s.SecondUserId == followingUserId);
                    if (existingSubscription == null)
                    {
                        throw new InvalidOperationException($"User with id {followerUserId} is not following user with id {followingUserId}.");
                    }

                    _photoSNDbContext.Subscriptions.Remove(existingSubscription);

                    await _photoSNDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        public async Task<List<GetSimpleUserDto>> GetFollowersAsync(int userId)
        {
            try
            {
                var user = await _photoSNDbContext.Users
                    .Include(u => u.Followers)
                        .ThenInclude(s => s.FirstUser)
                            .ThenInclude(u => u.Avatars)
                    .FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    throw new ArgumentException($"User with id { userId } does not exist.");
                }

                var followers = _mapper.Map<List<GetSimpleUserDto>>(user.Followers.Select(s => s.FirstUser));

                return followers;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<GetSimpleUserDto>> GetFollowingsAsync(int userId)
        {
            try
            {
                var user = await _photoSNDbContext.Users
                    .Include(u => u.Following)
                        .ThenInclude(s => s.SecondUser)
                            .ThenInclude(u => u.Avatars)
                    .FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    throw new ArgumentException($"User with id { userId } does not exist.");
                }

                var following = _mapper.Map<List<GetSimpleUserDto>>(user.Following.Select(s => s.SecondUser));

                return following;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
