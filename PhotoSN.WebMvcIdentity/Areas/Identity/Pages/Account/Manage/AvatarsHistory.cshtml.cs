using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhotoSN.WebMvcIdentity.Areas.Identity.Pages.Account.Manage
{
    public class AvatarsHistoryModel : PageModel
    {
        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
