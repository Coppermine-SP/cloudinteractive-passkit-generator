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

            //TemplateKey Validation check.
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
    }
}