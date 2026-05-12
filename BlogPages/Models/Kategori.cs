namespace KisselBlog.Models
{
    public class Kategori
    {
        public int Id { get; set; }
        public string Ad { get; set; } = "";
        public ICollection<BlogYazisi> BlogYazilari { get; set; } = new List<BlogYazisi>();
    }
}