using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoSN.Data.Entities;
using PhotoSN.Data.Repositories;
using PhotoSN.Model.Dtos;
using PhotoSN.Model.IdentityInputModels;
using PhotoSN.WebMvcIdentity.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoSN.WebMvcIdentity.Areas.Identity.Pages.Account.Manage
{
    public class AvatarsHistoryModel : PageModel
    {
        private readonly IPhotoSNRepository _photoSNRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IImageHelper _imageHelper;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public int ImageId { get; set; }

        [BindProperty]
        public bool IsOperationChange { get; set; }

        public List<AvatarsHistoryInputModel> AvatarsHistoryInputModels { get; set; }

        public AvatarsHistoryModel(
            IPhotoSNRepository photoSNRepository,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IImageHelper imageHelper)
        {
            _photoSNRepository = photoSNRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _imageHelper = imageHelper;
        }

        private async Task LoadAsync(User user)
        {
            AvatarsHistoryInputModels = await _photoSNRepository.GetAvatarsAsync(user.Id);
            AvatarsHistoryInputModels.Reverse();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (IsOperationChange)
            {
                await _photoSNRepository.ChangeCurrentAvatarAsync(new AvatarDto
                {
                    UserId = user.Id,
                    ImageId = ImageId
                });

                StatusMessage = "Your profile picture has been updated.";
            }
            else
            {
                await _photoSNRepository.DeleteAvatarAsync(new AvatarDto
                {
                    UserId = user.Id,
                    ImageId = ImageId
                });

                var imageToDelete = await _photoSNRepository.GetImageAsync(ImageId);
                await _photoSNRepository.DeleteImageAsync(ImageId);
                _imageHelper.DeleteImage(imageToDelete.Guid);

                StatusMessage = "Your profile picture has been deleted.";
            }

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage();
        }
    }
}
