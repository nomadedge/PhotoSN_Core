using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoSN.WebMvcIdentity.Models
{
    public class PostModel
    {
        [BindProperty]
        [MaxLength(300, ErrorMessage = "The {0} must be at max {1} characters long.")]
        public string Description { get; set; }

        [Display(Name = "Pictures")]
        public List<int> ImageIds { get; set; }
    }
}
