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
        [AutoValidateAntiforgeryToken]
        public IActionResult Index(IssueModel? model)
        {
            if(model == null) model = new IssueModel();

            //TemplateKey validation check.
            if (model.TemplateKey is not null && !model.TemplateKeyValidation())
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
        [AutoValidateAntiforgeryToken]
        public IActionResult Request(IssueModel? model)
        {
            //model validation check.
            if (model is not null && !model.TemplateKeyValidation())
            {
                return View("Error", new ErrorViewModel()
                {
                    ErrorId = "INVALID_REQUEST",
                    Message = "유효하지 않은 요청"
                });
            }

            return View("Home/Index");
        }
    }
}