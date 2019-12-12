using Library.BusinessLogic.Services.Interfaces;
using Library.BusinessLogic.Services.ViewModel.Books.Get;
using Library.BusinessLogic.Services.ViewModel.Books.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Library.WebApplication.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private ILogger<BooksController> _logger;
        public BooksController(IBookService bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            _logger.LogInformation($"TIME LOG HERE : {DateTime.UtcNow}");
            return Ok();
        }

        [HttpGet("books")]
        public IActionResult GetAllBooks()
        {
            var books = _bookService.GetBooks();
            return Ok(books);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]CreateBookViewModel createBookViewModel)
        {
            _bookService.Insert(createBookViewModel);
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(DeleteBooksViewModel deleteBooksViewModel)
        {
            _bookService.Delete(deleteBooksViewModel);
            return Ok();
        }

        [HttpPost("edit")]
        public IActionResult Edit([FromBody]EditBookViewModel editBookViewModel)
        {
            _bookService.Updata(editBookViewModel);
            return Ok(editBookViewModel);
        }

        [HttpPost("sort")]
        public IActionResult Sort ([FromBody]SortBooksByParamsView model)
        {
            var books = _bookService.SortByParamsAsync(model);
            return Ok(books);
        }
    }
}
