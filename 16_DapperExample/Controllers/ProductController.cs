using Microsoft.AspNetCore.Mvc;

namespace _16_DapperExample.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
