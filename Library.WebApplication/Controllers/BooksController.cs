using Library.BusinessLogic.Services.Interfaces;
using Library.BusinessLogic.Services.ViewModel;
using Library.BusinessLogic.Services.ViewModel.Books.Get;
using Library.BusinessLogic.Services.ViewModel.Books.Post;
using Library.DataAccess.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Sort ([FromBody] FilterRequest request)
        {
            List<BookWithAuthorsDto> books = await _bookService.SortByParamsAsync(request);
            return Ok(books);
        }
    }
}
