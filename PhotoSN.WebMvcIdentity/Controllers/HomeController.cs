using Microsoft.AspNetCore.Mvc;
using PhotoSN.WebMvcIdentity.ViewModels;
using System.Diagnostics;

namespace PhotoSN.WebMvcIdentity.Controllers
{
    //[Route("[controller]")]
    //[Controller]
    public class HomeController : Controller
    {
        //[HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
