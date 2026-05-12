using KisselBlog.Models;
using Microsoft.AspNetCore.Mvc;

namespace KisselBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogContext _context;

        public BlogController(BlogContext context)
        {
            _context = context;
        }

        // Tüm yazıları listele
        public IActionResult Index()
        {
            var yazilar = _context.BlogYazilari.ToList();
            return View(yazilar);
        }

        // Yeni yazı ekleme sayfası
        public IActionResult Ekle()
        {
            return View();
        }

        // Yeni yazıyı kaydet
        [HttpPost]
        public IActionResult Ekle(BlogYazisi yazi)
        {
            _context.BlogYazilari.Add(yazi);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        public IActionResult Sil(int id)
        {
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
            var yazi = _context.BlogYazilari.Find(id);
            if (yazi == null) return RedirectToAction("Index");
            return View(yazi);
        }

        // Düzenlenmiş yazıyı kaydet
        [HttpPost]
        public IActionResult Duzenle(BlogYazisi yazi)
        {
            var mevcutYazi = _context.BlogYazilari.Find(yazi.Id);
            if (mevcutYazi != null)
            {
                mevcutYazi.Baslik = yazi.Baslik;
                mevcutYazi.Icerik = yazi.Icerik;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}