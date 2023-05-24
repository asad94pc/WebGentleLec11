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
                new BookModel() {Id = 1, Author = "asad",  Title = "Java",       Category = "Programing",                 Language = "Pashto" ,     Pages = 421,  Description ="This is a java book."},
                new BookModel() {Id = 2, Author = "ahmad", Title = "NET",        Category = "Backend",                    Language = "Sanskrit" ,   Pages = 259,  Description ="This is a Dot NET book."},
                new BookModel() {Id = 3, Author = "ali",   Title = "CSS",        Category = "Front End",                  Language="Urdu",          Pages = 321,  Description ="This is a CSS book."},
                new BookModel() {Id = 4, Author = "abdul", Title = "HTML",       Category = "Hyper Text Markup Language", Language = "English",     Pages = 102,  Description ="This is an HTML book."},
                new BookModel() {Id = 5, Author = "asad",  Title = "Java",       Category = "Client Side Script",         Language= "German",       Pages = 118,  Description ="This is a java book."},
                new BookModel() {Id = 6, Author = "Anwar", Title = "BootStrap",  Category = "Designing",                  Language = "Hindi",       Pages = 125,  Description ="This is a  Bootstrap book."}
            };
        }
    }
}
