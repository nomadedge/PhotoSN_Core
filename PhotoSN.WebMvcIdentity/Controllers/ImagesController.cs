using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoSN.Data.Dtos;
using PhotoSN.Data.Entities;
using PhotoSN.Data.Repositories;
using PhotoSN.WebMvcIdentity.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoSN.WebMvcIdentity.Controllers
{
    public class ImagesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IPhotoSNRepository _photoSNRepository;
        private readonly IImageHelper _imageHelper;

        public ImagesController(
            UserManager<User> userManager,
            IPhotoSNRepository photoSNRepository,
            IImageHelper imageHelper)
        {
            _userManager = userManager;
            _photoSNRepository = photoSNRepository;
            _imageHelper = imageHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetImage(int id)
        {
            try
            {
                var getImageDto = await _photoSNRepository.GetImageAsync(id);
                var byteArray = await _imageHelper.ReadImageAsync(getImageDto.Guid);
                return File(byteArray, getImageDto.MimeType);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateImage(IFormFile imageFile)
        {
            try
            {
                if (imageFile.ContentType.Contains("image"))
                {
                    throw new ArgumentException("File type should be image");
                }
                var newGuid = Guid.NewGuid();
                var userId = (await _userManager.GetUserAsync(User)).Id;
                var createImageDto = new CreateImageDto
                {
                    UserId = userId,
                    MimeType = imageFile.ContentType,
                    Guid = newGuid
                };

                var imageId = await _photoSNRepository.CreateImageAsync(createImageDto);
                await _imageHelper.SaveImageAsync(imageFile, newGuid);

                return StatusCode(StatusCodes.Status201Created, imageId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateImages(IFormFileCollection imageFiles)
        {
            try
            {
                foreach (var imageFile in imageFiles)
                {
                    if (imageFile.ContentType.Contains("image"))
                    {
                        throw new ArgumentException("File type should be image");
                    }
                }

                var imageIds = new List<int>();
                var userId = (await _userManager.GetUserAsync(User)).Id;
                foreach (var imageFile in imageFiles)
                {
                    var newGuid = Guid.NewGuid();
                    var createImageDto = new CreateImageDto
                    {
                        UserId = userId,
                        MimeType = imageFile.ContentType,
                        Guid = newGuid
                    };

                    imageIds.Add(await _photoSNRepository.CreateImageAsync(createImageDto));
                    await _imageHelper.SaveImageAsync(imageFile, newGuid);
                }

                return StatusCode(StatusCodes.Status201Created, imageIds);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}