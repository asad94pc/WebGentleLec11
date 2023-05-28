using Lec11.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lec11.Repository
{
    public interface IBookRepository
    {
        Task<int> AddNewBook(BookModel book);
        Task<List<BookModel>> GetAllBooks();
        Task<BookModel> GetById(int id);
        Task<List<BookModel>> GetTopBooks(int count);
    }
}