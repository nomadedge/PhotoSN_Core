using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoSN.Data.DbContexts;
using PhotoSN.Data.Entities;
using PhotoSN.Model.Dtos;
using System;
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

        public async Task<int> CreateImageAsync(CreateImageDto createImageDto)
        {
            using (var transaction = _photoSNDbContext.Database.BeginTransaction())
            {
                try
                {
                    var newImage = _mapper.Map<Image>(createImageDto);
                    newImage.Guid = Guid.NewGuid();
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
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
