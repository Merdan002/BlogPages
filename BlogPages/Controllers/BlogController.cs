using KisselBlog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KisselBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogContext _context;

        public BlogController(BlogContext context)
        {
            _context = context;
        }

        private bool GirisYapildiMi()
        {
            return HttpContext.Session.GetString("KullaniciAdi") != null;
        }

        // Tüm yazıları listele
        public IActionResult Index(string? ara)
        {
            if (!GirisYapildiMi()) return RedirectToAction("Index", "Giris");
            var yazilar = _context.BlogYazilari.Include(x => x.Kategori).AsQueryable();

            if (!string.IsNullOrEmpty(ara))
                yazilar = yazilar.Where(x => x.Baslik.Contains(ara) || x.Icerik.Contains(ara));

            ViewBag.Ara = ara;
            return View(yazilar.OrderByDescending(x => x.Tarih).ToList());
        }

        // Yeni yazı ekleme sayfası
        public IActionResult Ekle()
        {
            if (!GirisYapildiMi()) return RedirectToAction("Index", "Giris");
            ViewBag.Kategoriler = _context.Kategoriler.ToList();
            return View();
        }

        // Yeni yazıyı kaydet
        [HttpPost]
        public IActionResult Ekle(BlogYazisi yazi)
        {
            if (!GirisYapildiMi()) return RedirectToAction("Index", "Giris");
            _context.BlogYazilari.Add(yazi);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        public IActionResult Sil(int id)
        {
            if (!GirisYapildiMi()) return RedirectToAction("Index", "Giris");
            var yazi = _context.BlogYazilari.Find(id);
            if (yazi != null)
            {
                _context.BlogYazilari.Remove(yazi);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Düzenleme sayfasını aç
        public IActionResult Duzenle(int id)
        {
            if (!GirisYapildiMi()) return RedirectToAction("Index", "Giris");
            var yazi = _context.BlogYazilari.Find(id);
            if (yazi == null) return RedirectToAction("Index");
            ViewBag.Kategoriler = _context.Kategoriler.ToList();
            return View(yazi);
        }

        // Düzenlenmiş yazıyı kaydet
        [HttpPost]
        public IActionResult Duzenle(BlogYazisi yazi)
        {
            if (!GirisYapildiMi()) return RedirectToAction("Index", "Giris");
            var mevcutYazi = _context.BlogYazilari.Find(yazi.Id);
            if (mevcutYazi != null)
            {
                mevcutYazi.Baslik = yazi.Baslik;
                mevcutYazi.Icerik = yazi.Icerik;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Herkese açık blog sayfası
        public IActionResult Anasayfa(string? ara)
        {
            var yazilar = _context.BlogYazilari.Include(x => x.Kategori).AsQueryable();

            if (!string.IsNullOrEmpty(ara))
                yazilar = yazilar.Where(x => x.Baslik.Contains(ara) || x.Icerik.Contains(ara));

            ViewBag.Ara = ara;
            return View(yazilar.OrderByDescending(x => x.Tarih).ToList());
        }

        // Yazı detay sayfası
        public IActionResult Detay(int id)
        {
            var yazi = _context.BlogYazilari.Find(id);
            if (yazi == null) return RedirectToAction("Anasayfa");
            return View(yazi);
        }
    }
}   