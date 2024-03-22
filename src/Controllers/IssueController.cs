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
    }
}