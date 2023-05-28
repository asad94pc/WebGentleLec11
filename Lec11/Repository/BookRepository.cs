using Lec11.Data;
using Lec11.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lec11.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _dbContext = null;

        public BookRepository(BookDbContext context)
        {
            _dbContext = context;
        }

        public async Task<int> AddNewBook(BookModel book)
        {
            var newBook = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                Pages = book.Pages,
                CoverImageUrl = book.CoverImageUrl,
                LanguageId = book.LanguageId,
                CreatedOn = book.CreatedOn,
                Category = book.Category,
                Author = book.Author,
                BookPdfUrl = book.BookPdfUrl,
            };
            newBook.BooksGallary = new List<BookGallary>();
            foreach (var file in book.Gallary)
            {
                newBook.BooksGallary.Add(new BookGallary()
                {
                    Name = file.Name,
                    Url = file.Url,
                });
            }
            await _dbContext.Books.AddAsync(newBook);
            await _dbContext.SaveChangesAsync();

            return newBook.Id;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            var bookList = new List<BookModel>();

            var books = await _dbContext.Books.ToListAsync();

            if (books.Any())
            {
                foreach (var book in books)
                {
                    var bookModel = new BookModel()
                    {
                        Title = book.Title,
                        Id = book.Id,
                        CreatedOn = book.CreatedOn,
                        Category = book.Category,
                        Author = book.Author,
                        LanguageId = book.LanguageId,
                        Description = book.Description,
                        Pages = book.Pages,
                        CoverImageUrl = book.CoverImageUrl,
                        BookPdfUrl = book.BookPdfUrl,
                    };
                    bookList.Add(bookModel);
                }
            }
            return bookList;
        }

        public async Task<List<BookModel>> GetTopBooks(int count)
        {
            var bookList = await _dbContext.Books.Select(book => new BookModel()
            {
                Title = book.Title,
                Id = book.Id,
                CreatedOn = book.CreatedOn,
                Category = book.Category,
                Author = book.Author,
                LanguageId = book.LanguageId,
                Description = book.Description,
                Pages = book.Pages,
                CoverImageUrl = book.CoverImageUrl,
                BookPdfUrl = book.BookPdfUrl,

            }).Take(count).ToListAsync();

            return bookList;
        }


        public async Task<BookModel> GetById(int id)
        {
            return await _dbContext.Books.Where(x => x.Id == id)
                .Select(book => new BookModel()
                {
                    Id = book.Id,
                    Author = book.Author,
                    Title = book.Title,
                    Description = book.Description,
                    Language = book.Language.Name,
                    Pages = book.Pages,
                    CreatedOn = book.CreatedOn,
                    Category = book.Category,
                    CoverImageUrl = book.CoverImageUrl,
                    Gallary = book.BooksGallary.Select(g => new GallaryModel()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        Url = g.Url
                    }).ToList(),
                    BookPdfUrl = book.BookPdfUrl,

                }).FirstOrDefaultAsync();

        }


    }
}
