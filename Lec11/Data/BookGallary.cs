namespace Lec11.Data
{
    public class BookGallary
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public Book Book { get; set; }  
    }
}
