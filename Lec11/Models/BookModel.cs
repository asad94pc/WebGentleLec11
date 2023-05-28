using Lec11.Enums;
using Lec11.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lec11.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        //[MyCustomValidation(ErrorMessage = "this is error message for my custom validation")]
       // [MyCustomValidation("abc")]
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int LanguageId { get; set; }
        public string Language { get; set; }
        public int Pages { get; set; }
        public DateTime? CreatedOn { get; set; }
        
        [Required]
        [Display(Name = "Please choose a cover photo")]
        public IFormFile CoverPhoto { get; set; }

        public string CoverImageUrl { get; set; }

        [Required]
        [Display(Name = "Please choose gallary images")]
        public IFormFileCollection GallaryFiles { get; set; }

        public List<GallaryModel> Gallary { get; set; }

        [Required]
        [Display(Name = "upload book in pdf")]
        public IFormFile BookPdf { get; set; }

        public string BookPdfUrl { get; set; }
    }
}
