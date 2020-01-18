using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoSN.Model.IdentityInputModels
{
    public class ManageIndexInputModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "The {0} must be alphanumeric.")]
        public string Nickname { get; set; }

        [Display(Name = "Private profile")]
        public bool IsPrivate { get; set; }

        [StringLength(300)]
        public string Bio { get; set; }

        [Display(Name = "Birth date")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? BirthDate { get; set; }
    }
}
