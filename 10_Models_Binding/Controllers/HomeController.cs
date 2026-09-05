using _10_Models_Binding.Models;
using _10_Models_Binding.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _10_Models_Binding.Controllers
{
    public class HomeController : Controller
    { 
        public IActionResult Index()
        {
            Kisi kisi = new Kisi()
            {
                Ad = "Tahsin",
                Soyad = "Canpolat",
                Yas = 35
            };

            return View(kisi); // kisi nesnesi Home/Index view ine yollanır.
        }

        [HttpPost]
        public IActionResult Index(Kisi kisi)
        {
            return View(kisi);
        }

        public IActionResult HomePage()
        {
            Kisi kisi = new Kisi()
            {
                Ad = "Ahmet",
                Soyad = "Demir",
                Yas = 40
            };

            Adres adres = new Adres()
            {
                Sehir = "İstanbul",
                AdresTanim = "Caferağa mahallesi Kadıköy"
            };
            // Sorun: Buradaki adres ve kisi adlı nesnelerin ikisininde view gönderilmek istenmesi ama gönderilememesi
            // Çözüm: iki nesneyi birleştirmek. Burada viewde kullanılacak nesneleri ayarlamak için ViewModel kavramını kullanılıyor.
            KisiAdres kisiAdres = new KisiAdres()
            {
                Kisi = kisi,
                Adres = adres,
            };
            
            return View(kisiAdres);
        }
       
    }
}
