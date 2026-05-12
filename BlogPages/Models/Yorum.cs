namespace KisselBlog.Models
{
    public class Yorum
    {
        public int Id { get; set; }
        public string YazarAdi { get; set; } = "";
        public string Icerik { get; set; } = "";
        public DateTime Tarih { get; set; } = DateTime.Now;
        public int BlogYazisiId { get; set; }
        public BlogYazisi? BlogYazisi { get; set; }
    }
}