using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _02_Controller_To_View.Controllers
{
    public class HomeController : Controller
    {
        // Controllerlardan View'e gitmek için action methodları kullanılır.
        // Bu action methodları IActionResult tipinde olmalıdır.
        // IActionResult tipinde olan methodlar View, Json, Html sayfası döndürebilir.
        public IActionResult Index() // Bu method View/Home/Index.cshtml dosyasını geriye döndürür.
        {
            List<string> products = new List<string>() { "Ürün 1", "Ürün 2", "Ürün 3" };
            // veriyi viewdata ile view'e gönderebiliriz.
            ViewData["products"] = products;
            return View();
        }



        public IActionResult Details(int id)
        {
            var product = $"Ürün {id}";
            ViewData["productDetail"] = product;
            return View();
        }

        
    }
}
