using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PhotoSN.Model.IdentityInputModels
{
    public class ManageAvatarInputModel
    {
        [Display(Name = "Current profile picture")]
        public int? AvatarImageId { get; set; }

        [Required]
        [Display(Name = "Upload new profile picture")]
        public IFormFile AvatarImage { get; set; }
    }
}
