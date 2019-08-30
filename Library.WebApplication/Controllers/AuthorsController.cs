
using AutoMapper;
using Library.BusinessLogic.Services.Interfaces;
using Library.BusinessLogic.Services.ViewModel.Authors.Get;
using Library.BusinessLogic.Services.ViewModel.Authors.Post;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Library.WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        public readonly IAuthorService _authorService;
        public readonly IMapper _mapper;
        public AuthorsController (IAuthorService authorService)
        {
            _authorService = authorService;
        }
        
        [HttpGet("authors")]
        public IActionResult Index()
         {
            var autrhos = _authorService.GetAuthors();
            return Ok(autrhos);
        } 

        [HttpGet]
        public IActionResult Create()
        {
            return Ok();
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody]CreateAuthorViewModel createAuthorViewModel)
        {
            _authorService.Insert(createAuthorViewModel);
            //return RedirectToAction("Index");
            return Ok(createAuthorViewModel);
        }


        [HttpGet]        
        public IActionResult Delete()
        {
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(DeleteAuthorsViewModel deleteAuthorsViewModel)
        {
            _authorService.Delete(deleteAuthorsViewModel);
            return RedirectToAction("Index");

        }


    }
}
