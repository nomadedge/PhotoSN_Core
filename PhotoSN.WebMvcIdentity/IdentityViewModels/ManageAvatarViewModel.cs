using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PhotoSN.WebMvcIdentity.IdentityViewModels
{
    public class ManageAvatarViewModel
    {
        [Display(Name = "Current profile picture")]
        public int? AvatarImageId { get; set; }

        [Required]
        [Display(Name = "Upload new profile picture")]
        public IFormFile AvatarImage { get; set; }
    }
}
