using KisselBlog.Models;
using Microsoft.AspNetCore.Mvc;

namespace KisselBlog.Controllers
{
    public class GirisController : Controller
    {
        private readonly BlogContext _context;

        public GirisController(BlogContext context)
        {
            _context = context;
        }

        // Giriş sayfasını göster
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("KullaniciAdi") != null)
                return RedirectToAction("Index", "Blog");

            return View();
        }

        // Giriş yap
        [HttpPost]
        public IActionResult Index(string kullaniciAdi, string sifre)
        {
            var kullanici = _context.Kullanicilar
                .FirstOrDefault(k => k.KullaniciAdi == kullaniciAdi && k.Sifre == sifre);

            if (kullanici != null)
            {
                HttpContext.Session.SetString("KullaniciAdi", kullanici.KullaniciAdi);
                return RedirectToAction("Index", "Blog");
            }

            ViewBag.Hata = "Kullanıcı adı veya şifre hatalı!";
            return View();
        }

        // Çıkış yap
        public IActionResult Cikis()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        // İlk kullanıcıyı oluştur (sadece bir kez kullan!)
        public IActionResult KurulumYap()
        {
            if (_context.Kullanicilar.Any())
                return Content("Kullanıcı zaten mevcut!");

            _context.Kullanicilar.Add(new Kullanici
            {
                KullaniciAdi = "admin",
                Sifre = "1234"
            });
            _context.SaveChanges();
            return Content("Kullanıcı oluşturuldu! admin / 1234 ile giriş yapabilirsin.");
        }

        // Şifre değiştirme sayfası
        public IActionResult SifreDegistir()
        {
            if (HttpContext.Session.GetString("KullaniciAdi") == null)
                return RedirectToAction("Index");
            return View();
        }

        // Şifre değiştirmeyi kaydet
        [HttpPost]
        public IActionResult SifreDegistir(string eskiSifre, string yeniSifre, string yeniSifreTekrar)
        {
            if (HttpContext.Session.GetString("KullaniciAdi") == null)
                return RedirectToAction("Index");

            var kullaniciAdi = HttpContext.Session.GetString("KullaniciAdi");
            var kullanici = _context.Kullanicilar.FirstOrDefault(k => k.KullaniciAdi == kullaniciAdi);

            if (kullanici == null)
            {
                ViewBag.Hata = "Kullanıcı bulunamadı!";
                return View();
            }

            if (kullanici.Sifre != eskiSifre)
            {
                ViewBag.Hata = "Mevcut şifre hatalı!";
                return View();
            }

            if (yeniSifre != yeniSifreTekrar)
            {
                ViewBag.Hata = "Yeni şifreler eşleşmiyor!";
                return View();
            }

            if (yeniSifre.Length < 4)
            {
                ViewBag.Hata = "Şifre en az 4 karakter olmalı!";
                return View();
            }

            kullanici.Sifre = yeniSifre;
            _context.SaveChanges();

            ViewBag.Basari = "Şifre başarıyla değiştirildi!";
            return View();
        }
    }
}