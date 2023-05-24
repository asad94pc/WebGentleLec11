using Lec11.Models;
using Lec11.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Lec11.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _book = null;

        public BookController()
        {
            _book = new BookRepository();

        }
        public ViewResult GetAllBooks()
        {
            var data = _book.GetAllBooks();
            return View(data);
        }

        public BookModel GetBook(int id)
        {
            return _book.GetById(id);
        }

       

        public string SearchBook(int id, string author)
        {
            return $"Book ID: {id}, Book Author: {author}";
        }
    }
}
