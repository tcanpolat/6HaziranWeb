using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _08_View_PartialView_Layout.Controllers
{
    public class HomeController : Controller
    {  
        public IActionResult Index()    // Home/Index
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }
       
    }
}
