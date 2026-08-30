using _06_HtmlHelpers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace _06_HtmlHelpers.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            User model = new User()
            {
                CountryList = GetCountries()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Submit(User model)
        {
            User user = new User()
            {
                CountryList = GetCountries()
            };

            if (ModelState.IsValid)
            {
                ViewBag.Message = $"Merhaba {model.Name} kaydınız başarıyla gerçekleşti.";
                return View("Result",model);
            }
            return View("Index",user);
        }

        public List<SelectListItem> GetCountries()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text = "United State",Value = "USA"},
                new SelectListItem{Text = "Türkiye",Value = "TR"},
                new SelectListItem{Text = "Germany",Value = "DE"},

            };
        }

    }
}
