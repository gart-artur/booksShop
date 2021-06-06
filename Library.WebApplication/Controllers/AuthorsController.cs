using Library.BusinessLogic.Services.Interfaces;
using Library.BusinessLogic.Services.ViewModel;
using Library.BusinessLogic.Services.ViewModel.Authors.Get;
using Library.BusinessLogic.Services.ViewModel.Authors.Post;
using Library.DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        
        [HttpPost("authors")]
        public async Task<IActionResult> GetAllAuthors([FromBody] FilterRequest request)
        {
            List<AuthorWithBooks> autrhos = await _authorService.GetByFilter(request);
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
            return Ok();
        }
    }
}
