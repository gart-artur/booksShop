using Library.BusinessLogic.Services.Interfaces;
using Library.BusinessLogic.Services.ViewModel.Books.Get;
using Library.BusinessLogic.Services.ViewModel.Books.Post;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApplication.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
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
