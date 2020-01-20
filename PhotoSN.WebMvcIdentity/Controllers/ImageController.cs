using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoSN.Data.Repositories;
using PhotoSN.WebMvcIdentity.Services;
using System;
using System.Threading.Tasks;

namespace PhotoSN.WebMvcIdentity.Controllers
{
    public class ImageController : Controller
    {
        private readonly IPhotoSNRepository _photoSNRepository;
        private readonly IImageHelper _imageHelper;

        public ImageController(
            IPhotoSNRepository photoSNRepository,
            IImageHelper imageHelper)
        {
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
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}