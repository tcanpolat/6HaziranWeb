using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _04_ViewBag_ViewData_TempData.Controllers
{
    public class HomeController : Controller
    {
        /* 
         Controller'da ViewBag,ViewData,TempData kullanımı:
         1. ViewBag: Dinamik bir nesnedir ve verileri key-value çiftleri olarak saklar.
         2. ViewData:Sözlük (Dictionary) tabanlıdır ve verileri key-value çiftleri olarak saklar.
         3. TempData: Verileri bir istek diğer bir isteğe taşımak için kullanılır. İki istek arasında
         veri saklar ve taşır.
         
         
         */
        public IActionResult Index()
        {
            // ViewBag dinamik özellikler alır ve bunun sayesinde herhangi bir türde veri saklayabiliriz.
            // Bu veriler sadece mevcut istek süresi boyunca (yani bir action method çağrısı sırasında) geçerlidir.
            // Tanımlama
            ViewBag.ad = "Tahsin";
            ViewBag.sonuc = true;
            ViewBag.yas = 30;
            List<string> renkler = new List<string>() { "Kırmızı", "Mavi", "Yeşil" };
            ViewBag.liste = renkler;

            // ViewData, ViewBag'e benzer şekilde çalışır ancak sözlük tabanlıdır. Veri 1 sonuç boyunca geçerlidir.
            // Tanımlama
            ViewData["ad"] = "Ayşe";
            ViewData["sonuc"] = false;
            ViewData["sayi"] = 30;

            // TempData, verileri bir istek diğerine taşımak için kullanılır.
            TempData["mesaj"] = "İşlem başarılı";
            
            return View();
        }

        public IActionResult About()
        {
            TempData["message"] = TempData["mesaj"];
            return View();
        }
  
    }
}
