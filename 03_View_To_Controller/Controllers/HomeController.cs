using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _03_View_To_Controller.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet] // Bu attribute ile bu methodun sadece GET istekleri için çalışacağını belirtiyoruz.
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost] // Bu attribute ile bu methodun sadece POST istekleri için çalışacağını belirtiyoruz.
        public IActionResult KisiGonder(string ad, string kisiler,bool onay)
        {
            return Redirect("Index");
        }

    }
}
