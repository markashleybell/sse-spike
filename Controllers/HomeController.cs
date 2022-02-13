using Microsoft.AspNetCore.Mvc;
using sse_spike.Models;

namespace sse_spike.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new IndexViewModel();

            return View(model);
        }
    }
}
