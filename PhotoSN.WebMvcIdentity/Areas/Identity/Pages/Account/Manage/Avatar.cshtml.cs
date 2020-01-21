using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoSN.Data.Entities;
using PhotoSN.Data.Repositories;
using PhotoSN.Model.Dtos;
using PhotoSN.Model.IdentityInputModels;
using PhotoSN.WebMvcIdentity.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoSN.WebMvcIdentity.Areas.Identity.Pages.Account.Manage
{
    public partial class AvatarModel : PageModel
    {
        private readonly IPhotoSNRepository _photoSNRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IImageHelper _imageHelper;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public ManageAvatarInputModel Input { get; set; }

        public AvatarModel(
            IPhotoSNRepository photoSNRepository,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IImageHelper imageHelper)
        {
            _photoSNRepository = photoSNRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _imageHelper = imageHelper;

            Input = new ManageAvatarInputModel();
        }

        private void Load(User user)
        {
            var avatarIds = _photoSNRepository.GetAvatars(user.Id);
            if (avatarIds.Any())
            {
                Input.AvatarImageId = avatarIds.Last();
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Load(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                Load(user);
                return Page();
            }

            var guid = Guid.NewGuid();
            await _imageHelper.SaveImageAsync(Input.AvatarImage, guid);

            var createImageDto = new CreateImageDto
            {
                Guid = guid,
                UserId = user.Id,
                MimeType = Input.AvatarImage.ContentType
            };
            var newImageId = await _photoSNRepository.CreateImageAsync(createImageDto);

            var createAvatarDto = new CreateAvatarDto
            {
                UserId = user.Id,
                ImageId = newImageId
            };
            await _photoSNRepository.CreateAvatarAsync(createAvatarDto);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile picture has been updated";
            return RedirectToPage();
        }
    }
}
