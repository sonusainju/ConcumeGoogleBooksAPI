using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1_GoogleAPIBooks.Models
{
    public class Book
    {

        public string id { get; set; }

        [Display(Name = "Title")]
        public string title { get; set; }

        [Display(Name = "Image")]
        public string smallThumbnail { get; set; }

        [Display(Name = "Authors")]
        public string authors { get; set; }

        [Display(Name = "Publisher")]
        public string publisher { get; set; }

        [Display(Name = "PublishedDate")]
        public string publishedDate { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }
       
        [Display(Name = "Isbn_10")]
        public string ISBN_10 { get; set; }

    }
}
