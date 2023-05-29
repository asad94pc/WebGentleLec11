using Lec11.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lec11.Data
{
    public class BookDbContext : IdentityDbContext<ApplicationUser>
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<BookGallary> BooksGallary { get; set; }
    }
}
