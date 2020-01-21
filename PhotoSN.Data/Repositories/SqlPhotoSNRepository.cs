using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoSN.Data.DbContexts;
using PhotoSN.Data.Entities;
using PhotoSN.Model.Dtos;
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

        public async Task<List<int>> GetAvatarsAsync(int userId)
        {
            var avatarIds = await _photoSNDbContext.Avatars
                .Where(a => a.UserId == userId)
                .Select(a => a.ImageId)
                .ToListAsync();
            return avatarIds;
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

                    transaction.Commit();
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

                    transaction.Commit();
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
            using (var transaction = _photoSNDbContext.Database.BeginTransaction())
            {
                try
                {
                    var newImage = _mapper.Map<Image>(createImageDto);
                    newImage.User = await _photoSNDbContext.Users.FindAsync(createImageDto.UserId);
                    newImage.Created = DateTime.Now;

                    await _photoSNDbContext.Images.AddAsync(newImage);

                    await _photoSNDbContext.SaveChangesAsync();

                    transaction.Commit();

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

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }
    }
}
