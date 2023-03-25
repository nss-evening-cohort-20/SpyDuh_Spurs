using Microsoft.AspNetCore.Mvc;
using SpyDuh.Repositories;
using SpyDuh.Models;

namespace SpyDuh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandlerController : ControllerBase
    {
        private readonly IHandlerRepository _handlerRepository;

        public HandlerController(IHandlerRepository handlerRepository)
        {
            _handlerRepository = handlerRepository;
        }
        [HttpGet]
        public IActionResult Get(int id)

        {
            return Ok(_handlerRepository.listSpies(id));
        }
    }
}
