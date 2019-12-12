using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class ElasticController : Controller
    {
        private readonly IElasticsearchService _elasticsearchService;

        public ElasticController(IElasticsearchService elasticsearchService)
        {
            _elasticsearchService = elasticsearchService;
        }

        [HttpGet("index")]
        [AllowAnonymous]
        public async Task<IActionResult> IndexPage()
        {
            await _elasticsearchService.Run();
            return Ok();
        }
    }
}