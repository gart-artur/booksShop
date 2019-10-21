using Library.BusinessLogic.Services.Interfaces;
using Library.BusinessLogic.Services.ViewModel.Books.Get;
using Library.BusinessLogic.Services.ViewModel.Books.Post;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            return RedirectToAction("Index");
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(DeleteBooksViewModel deleteBooksViewModel)
        {
            _bookService.Delete(deleteBooksViewModel);
            return RedirectToAction("Index");

        }

        [HttpPost("edit")]
        public IActionResult Edit([FromBody]EditBookViewModel editBookViewModel)
        {
            _bookService.Updata(editBookViewModel);
            return Ok(editBookViewModel);
        }
    }
}
