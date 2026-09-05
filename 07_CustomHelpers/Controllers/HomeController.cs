using _07_CustomHelpers.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _07_CustomHelpers.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }       
    }
}
