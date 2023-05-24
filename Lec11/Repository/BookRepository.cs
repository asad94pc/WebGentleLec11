using Lec11.Models;
using System.Collections.Generic;
using System.Linq;

namespace Lec11.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
            
        }
        public BookModel GetById(int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
            
        }

        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel() {Id = 1, Author = "asad", Title = "Java"},
                new BookModel() {Id = 2, Author = "ahmad", Title = "NET"},
                new BookModel() {Id = 3, Author = "ali", Title = "CSS"},
                new BookModel() {Id = 4, Author = "abdul", Title = "HTML"}
            };
        }
    }
}
