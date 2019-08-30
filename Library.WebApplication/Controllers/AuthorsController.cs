
using AutoMapper;
using Library.BusinessLogic.Services.Interfaces;
using Library.BusinessLogic.Services.ViewModel.Authors.Get;
using Library.BusinessLogic.Services.ViewModel.Authors.Post;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        public readonly IAuthorService _authorService;
        public AuthorsController (IAuthorService authorService)
        {
            _authorService = authorService;
        }
        
        [HttpGet("authors")]
        public IActionResult GetAllAuthors()
        {
            var autrhos = _authorService.GetAuthors();
            return Ok(autrhos);
        }    

        [HttpPost("create")]
        public IActionResult Create([FromBody]CreateAuthorViewModel createAuthorViewModel)
        {
            _authorService.Insert(createAuthorViewModel);
            return Ok(createAuthorViewModel);
        }   
        
        [HttpPost("delete/{id}")]
        public IActionResult Delete(DeleteAuthorsViewModel deleteAuthorsViewModel)
        {
            _authorService.Delete(deleteAuthorsViewModel);
            return RedirectToAction("Index");

        }


    }
}
