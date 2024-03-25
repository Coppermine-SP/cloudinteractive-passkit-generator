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

        [Route("Check")]
        public IActionResult Check(string code)
        {
            if (String.IsNullOrWhiteSpace(code) || !Services.PassKit.PassKitGenerator.CheckPass(code))
            {
                return View("AuthFailed");
            }
            else
            {
                ViewBag.passId = code;
                return View("AuthSuccess");
            }
        }

        [Route("Get")]
        public IActionResult Get(string code)
        {
            byte[] pass;
            if (!String.IsNullOrWhiteSpace(code) && Services.PassKit.PassKitGenerator.GetPass(code, out pass))
            {
                return new FileContentResult(pass, "application/vnd.apple.pkpass");
            }

            return new BadRequestResult();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel());
        }
    }
}