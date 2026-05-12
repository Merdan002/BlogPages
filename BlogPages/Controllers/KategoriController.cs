using KisselBlog.Models;
using Microsoft.AspNetCore.Mvc;

namespace KisselBlog.Controllers
{
    public class KategoriController : Controller
    {
        private readonly BlogContext _context;

        public KategoriController(BlogContext context)
        {
            _context = context;
        }

        private bool GirisYapildiMi()
        {
            return HttpContext.Session.GetString("KullaniciAdi") != null;
        }

        // Kategorileri listele
        public IActionResult Index()
        {
            if (!GirisYapildiMi()) return RedirectToAction("Index", "Giris");
            var kategoriler = _context.Kategoriler.ToList();
            return View(kategoriler);
        }

        // Kategori ekle
        [HttpPost]
        public IActionResult Ekle(string ad)
        {
            if (!GirisYapildiMi()) return RedirectToAction("Index", "Giris");
            if (!string.IsNullOrEmpty(ad))
            {
                _context.Kategoriler.Add(new Kategori { Ad = ad });
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Kategori sil
        public IActionResult Sil(int id)
        {
            if (!GirisYapildiMi()) return RedirectToAction("Index", "Giris");
            var kategori = _context.Kategoriler.Find(id);
            if (kategori != null)
            {
                _context.Kategoriler.Remove(kategori);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}