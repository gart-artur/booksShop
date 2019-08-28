
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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        public readonly IAuthorService _authorService;
        public readonly IMapper _mapper;
        public AuthorsController (IAuthorService authorService)
        {
            _authorService = authorService;
        }
        // GET: /<controller>/
        [HttpGet("authors")]
        public IActionResult Index()
         {
            var autrhos = _authorService.GetAuthors();
            return Ok(autrhos);
        } 

        [HttpGet]
        //[Authorize(Roles = "Administrator")]

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
