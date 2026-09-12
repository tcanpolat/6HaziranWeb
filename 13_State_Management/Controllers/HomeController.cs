using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _13_State_Management.Controllers
{
    public class HomeController : Controller
    {
        /* 
         Session ve Cookie
         * Session (Oturum)
         * Kullanıcıya özel verileri sınucu tarafıda saklamak için kullanılır. Oturum sona
         * erdiğinde (örneğin tarayıcı kaptıldığında ya da kullanıcı logout olduğu) verilerg
         * genellikle silinir. Sessionlar kullanıcı kimlik doğrulaması yada kullanıcı sepet bilgileri
         * gibi verileri tutmak için uygundur. SUNUCU TARAFINDA SAKLANIR !
         * Cookie (Çerez)
         * Kullanıcıya ait bilgileri istemci (kullanıcı) tarafında (web siteleri için tarayıcıda)
         * saklar. Cookiler yapılan ayara göre belirli bir süre saklanabilir. Geçerli cookie
         * süresi dolduğunda ya da kullanıcı sildiğinde yok olur. Cookie ile kritik olmayan bilgiler
         * Kullanıcı tarafında saklandığı için güvenlik riskler taşır.
         * KULLANICI TARAFINDA SAKLANIR!
         
         
         
         
         */
        
        public IActionResult Index()
        {
            // Session oluşturma
            // key value ilişkisiyle saklanır.
            HttpContext.Session.SetString("KullaniciAdi","AdminUser");

            // Cookie ayarları
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(10), // Cookie geçerlilik süresi
                HttpOnly = true, // sadece http isteklerinde erişilebilir
                IsEssential = true // GDPR (KVKK) için gereklidir.
            };

            // Cookieyi set etme
            Response.Cookies.Append("KullaniciEmail", "tahsincanpolat@gmail.com");
            return View();
        }

        public IActionResult Privacy()
        {
            // session okuma
            var kullaniciAdi = HttpContext.Session.GetString("KullaniciAdi");
            ViewBag.KullaniciAdi = kullaniciAdi;

            // cookie okuma
            var kullaniciEmail = Request.Cookies["KullaniciEmail"];
            ViewBag.kullaniciEmail = kullaniciEmail;
            return View();
        }

        
    }
}
