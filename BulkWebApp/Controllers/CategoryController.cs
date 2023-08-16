using Microsoft.AspNetCore.Mvc;

namespace BulkWebApp.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
