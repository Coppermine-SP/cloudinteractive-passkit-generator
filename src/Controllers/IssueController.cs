using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Cloudinteractive.PassKitGenerator.Models;

namespace Cloudinteractive.PassKitGenerator.Controllers
{
    public class IssueController : Controller
    {
        private readonly ILogger<IssueController> _logger;

        public IssueController(ILogger<IssueController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(IssueModel? model)
        {
            if(model == null) model = new IssueModel();

            //TemplateKey validation check.
            if (model.TemplateKey is not null && !model.Validation())
            {
                return View("Error", new ErrorViewModel()
                {
                    ErrorId = "INVALID_TEMPLATE_KEY",
                    Message = "유효하지 않은 탬플릿 키."
                });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Request(IssueModel? model)
        {
            //model validation check.
            if (model is not null && !model.Validation())
            {
                return View("Error", new ErrorViewModel()
                {
                    ErrorId = "INVALID_REQUEST",
                    Message = "유효하지 않은 요청"
                });
            }

            try
            {
                string passId = Services.PassKit.PassKitGenerator.MakePass(model);
                ViewBag.passId = passId;
                return View("Success");
            }
            catch(Exception e)
            {
                _logger.Log(LogLevel.Error, "PassKitGenerator Exception: " + e.ToString());
                return View("Error", new ErrorViewModel()
                {
                    ErrorId = "EXCEPTION_GENERATOR",
                    Message = "발급 중 오류 발생"
                });
            }
        }
    }
}