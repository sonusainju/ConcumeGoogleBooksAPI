using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Assignment1_GoogleAPIBooks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Assignment1_GoogleAPIBooks.Controllers
{
    [Authorize]

    public class BooksController : Controller
    {
        const string BASE_URL = "https://www.googleapis.com/books/v1/volumes";
        private readonly ILogger<BooksController> _logger;
        public IEnumerable<Book> Books { get; set; }
        public bool GetStudentsError { get; private set; }
        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()

        {
            HttpClient client = new HttpClient();

            var request = "https://www.googleapis.com/books/v1/volumes?q=harry+potter";
            var response = await client.GetAsync(request);

            ArrayList books = new ArrayList();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                dynamic myList = Newtonsoft.Json.Linq.JObject.Parse(content);

                var items = myList.items;

                foreach (var item in items)
                {
                    Book newBook = new Book();

                    newBook.id = item.id;
                    newBook.title = item.volumeInfo.title;
                    newBook.smallThumbnail = item.volumeInfo.imageLinks.smallThumbnail;
                    newBook.authors = item.volumeInfo.authors.ToString();
                    newBook.publisher = item.volumeInfo.publisher;
                    newBook.publishedDate = item.volumeInfo.publishedDate;
                    newBook.description = item.volumeInfo.description;
                    newBook.ISBN_10 = item.volumeInfo.industryIdentifiers[1].indentifier;
                    books.Add(newBook);
                }
                Books = books.Cast<Book>();
            }
            else
            {
                GetStudentsError = true;
                Books = Array.Empty<Book>();
            }

            ViewBag.Books = Books;
            return View(Books);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri($"{BASE_URL}/{id}");
            message.Headers.Add("Accept", "application/json");


            HttpClient client = new HttpClient();

            var request = $"{BASE_URL}/{id}"; //"https://www.googleapis.com/books/v1/volumes?q=harry+potter";     

            var response = await client.GetAsync(request);

            Book newBook = null;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                dynamic myBook = Newtonsoft.Json.Linq.JObject.Parse(content);

                newBook = new Book();

                newBook.title = myBook.volumeInfo.title;
                newBook.smallThumbnail = myBook.volumeInfo.imageLinks.smallThumbnail;
                newBook.authors = myBook.volumeInfo.authors.ToString();
                newBook.publisher = myBook.volumeInfo.publisher;
                newBook.publishedDate = myBook.volumeInfo.publishedDate;
                newBook.description = myBook.volumeInfo.description;
                newBook.ISBN_10 = myBook.volumeInfo.industryIdentifiers[1].indentifier;
            }
            else
            {
                GetStudentsError = true;
            }

            if (newBook == null)
            {
                return NotFound();
            }

            return View(newBook);
        }


    }
}