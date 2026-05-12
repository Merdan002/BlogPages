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
    }
}