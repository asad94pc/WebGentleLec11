using Lec11.Models;
using Lec11.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Lec11.Controllers
{
    [Route("[controller]/[action]")]
    public class BookController : Controller
    {
        [ViewData]
        public string Title { get; set; }

        private readonly IBookRepository _book = null;
        private readonly ILanguageRepository _lang = null;
        private readonly IWebHostEnvironment _webHostEnvironment = null;


        public BookController(IBookRepository repository, ILanguageRepository lang, IWebHostEnvironment webHostEnvironment)
        {
            _book = repository;
            _lang = lang;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("~/AllBooks", Name = "AllBooks", Order = 1)]
        public async Task<ViewResult> GetAllBooks()
        {

            var data = await _book.GetAllBooks();
            return View(data);
        }

        [Route("~/SingleBook/{id:int:Min(1)}")]
        public async Task<ViewResult> GetBook(int id)
        {
            var book = await _book.GetById(id);
            return View(book);

        }



        public string SearchBook(int id, string author)
        {
            return $"Book ID: {id}, Book Author: {author}";
        }

        public async Task<ViewResult> AddBook(bool isSuccess = false, int bookId = 0)
        {
            var model = new BookModel();

            //ViewBag.Languages = new SelectList(await _lang.GetAllLanguages(), "Id", "Name");

            ViewBag.BookAdded = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookModel bookModel)
        {
            //ViewBag.Languages = new SelectList(await _lang.GetAllLanguages(), "Id", "Name");

            if (ModelState.IsValid)
            {
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    bookModel.CoverImageUrl = await UploadImage(folder, bookModel.CoverPhoto);
                }

                if (bookModel.GallaryFiles != null)
                {
                    string folder = "books/Gallary/";
                    bookModel.Gallary = new List<GallaryModel>();

                    foreach (var file in bookModel.GallaryFiles)
                    {
                        var gallaryModel = new GallaryModel()
                        {
                            Name = file.FileName,
                            Url = await UploadImage(folder, file)
                        };
                        bookModel.Gallary.Add(gallaryModel);
                        
                    }
                }

                if (bookModel.BookPdf != null)
                {
                    string folder = "books/pdf/";
                    bookModel.BookPdfUrl = await UploadImage(folder, bookModel.BookPdf);
                }

                int id = await _book.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddBook), new { isSuccess = true, bookId = id });
                }
            }

            return View(); 
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;

        }



    }
}
