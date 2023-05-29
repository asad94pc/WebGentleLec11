using Lec11.Models;
using Lec11.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Dynamic;

namespace Lec11.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly BookAlertConfigModel _configuration;
        private readonly BookAlertConfigModel _configurationThirdBook;
        private readonly IMessageRepository _messageRepository;
        public HomeController(IOptionsSnapshot<BookAlertConfigModel> configuration, IMessageRepository messageRepository)
        {
            _configuration = configuration.Get("MyBook1");
            _configurationThirdBook = configuration.Get("MyBook2");
            _messageRepository = messageRepository;
        }

        [ViewData]
        public string CustomProperty { get; set; }
        [ViewData]
        public BookModel Book { get; set; }
        [Route("~/")]
        public ViewResult Index()
        {
            bool isTrue = _configuration.NewBookRelease;
            string bookName = _configuration.NewBookName;

            bool isTrue2 = _configurationThirdBook.NewBookRelease;
            string bookName2 = _configurationThirdBook.NewBookName;
            var value = _messageRepository.GetName();


            //var bookBindModel = new BookAlertConfigModel();
            //_configuration.Bind("MyNewBook", bookBindModel);

            //bool isReleased = bookBindModel.NewBookRelease;
            //string bookName = bookBindModel.NewBookName;

            //var myAlert = _configuration.GetValue<bool>("MyAlert");
            //var key1 = _configuration["MyAlert"];
            //var key2 = _configuration["key2"];
            //var key3 = _configuration["key3:key3obj"];

            Book = new BookModel();
            Book.Id = 24;
            Book.Title = "ViewDataTitle for checking";
            CustomProperty = "My Custom Property";

            //ViewData["book"] = new BookModel() {Id = 9, Author = "Asadullah Tariq" };
            //ViewBag.Type = new BookModel() { Id = 11, Author = "Author Name" };
            //dynamic data = new ExpandoObject();
            //data.Id = 1;
            //data.Name = "Asad";
            //ViewBag.Data = data;

            return View();
        }
        //[Route("~/About/{name:alpha:MinLength(5)}")]
        public ViewResult AboutUs()
        {
            return View();
        }
        [Route("~/Contact")]
        public ViewResult ContactUs()
        {
            return View();
        }
    }
}
