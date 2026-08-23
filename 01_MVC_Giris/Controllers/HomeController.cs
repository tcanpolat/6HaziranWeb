using _01_MVC_Giris.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _01_MVC_Giris.Controllers
{
    // Homecontroller Controller sınıfından miras alır ve bir kontrolcüdür.
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Constructor => İnşa edici method.
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Index methodu geriye değer döndüren bir method.
        // IActinoResult döndüren bir method => IActionResult => View,Json,Html sayfası döndürebilir.
        // Bu method View/Home/Index.cshtml dosyasını geriye döndürür.
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {

            return View();
        }

    }
}
