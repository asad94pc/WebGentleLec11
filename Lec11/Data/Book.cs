using System;
using System.Collections.Generic;

namespace Lec11.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int LanguageId { get; set; }
        public int Pages { get; set; }
        public DateTime? CreatedOn { get; set; }

        public Language Language { get; set; }
        public string CoverImageUrl { get; set; }

        public string BookPdfUrl { get; set; }

        public ICollection<BookGallary> BooksGallary { get; set; }
    }
}
