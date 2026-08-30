using _05_ModelsExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace _05_ModelsExample.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            User user = new User()
            {
                Name = "Tahsin",
                Surname = "Canpolat",
                Age = 34
            };

            // Viewbag ile modeli viewe yollama
            ViewBag.User = user;    

            // Modeli doğrudan viewe yollama
            return View(user);
        }

        public IActionResult ProductPage()
        {
            Product product1 = new Product()
            {
                Id = 1,
                Name = "Laptop",
                Description = "Yüksek performanslı işlemci",
                Price = 15000.00m
            };

            Product product2 = new Product()
            {
                Id = 2,
                Name = "Akıllı Telefon",
                Description = "Iphone 17 pro",
                Price = 130000.00m
            };

            List<Product> products = new List<Product>();
            products.Add(product1);
            products.Add(product2);

            ViewBag.Products = products; // ViewBag ile modelleri liste halinde viewe yolluyoruz

            return View();
        }
    }
}
