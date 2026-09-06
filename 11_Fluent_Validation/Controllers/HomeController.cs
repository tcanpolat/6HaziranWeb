using _11_Fluent_Validation.Models;
using _11_Fluent_Validation.ViewModels;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _11_Fluent_Validation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IValidator<KisiAdresViewModel> _validator;

        public HomeController(IValidator<KisiAdresViewModel> validator)
        {
            _validator = validator;
        }

        public IActionResult Index()
       {
            KisiAdresViewModel model = new KisiAdresViewModel()
            {
                Kisi = new Kisi(),
                Adres = new Adres(),
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Submit(KisiAdresViewModel model)
        {
            ValidationResult result = _validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }

                return View("Index", model);
            }
            return View("Success", model);
        }

        public IActionResult Success(KisiAdresViewModel model)
        {
            return View(model);
        }
    }
}
