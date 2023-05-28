using Microsoft.EntityFrameworkCore;

namespace Lec11.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<BookGallary> BooksGallary { get; set; }
    }
}
