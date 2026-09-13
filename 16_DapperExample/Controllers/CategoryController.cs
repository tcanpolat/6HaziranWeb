using Microsoft.AspNetCore.Mvc;

namespace _16_DapperExample.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
