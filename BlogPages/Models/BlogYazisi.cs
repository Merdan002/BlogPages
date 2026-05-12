namespace KisselBlog.Models
{
    public class BlogYazisi
    {
        public int Id { get; set; }
        public string Baslik { get; set; } = "";
        public string Icerik { get; set; } = "";
        public DateTime Tarih { get; set; } = DateTime.Now;
        public int? KategoriId { get; set; }
        public Kategori? Kategori { get; set; }
    }
}