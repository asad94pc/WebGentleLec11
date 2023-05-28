using Lec11.Data;
using Lec11.Models;
using Lec11.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lec11.Components
{
    public class TopBooksViewComponent : ViewComponent
    {
        private readonly IBookRepository _repository;

        public TopBooksViewComponent(IBookRepository repository)
        {
            _repository = repository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int count = 3)
        {
            var bookData = await _repository.GetTopBooks(count);
            return View(bookData);
        }
    }
}
