using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sse_spike.Models;

namespace sse_spike.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) =>
            _logger = logger;

        public IActionResult Index()
        {
            var model = new IndexViewModel();

            return View(model);
        }

        public IActionResult Broadcast()
        {
            var model = new Broadcast();

            return View(model);
        }
    }
}
