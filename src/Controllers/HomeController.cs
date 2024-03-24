using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Cloudinteractive.PassKitGenerator.Models;

namespace Cloudinteractive.PassKitGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Auth")]
        public IActionResult Auth(string code)
        {
            return View("AuthFailed");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel());
        }
    }
}