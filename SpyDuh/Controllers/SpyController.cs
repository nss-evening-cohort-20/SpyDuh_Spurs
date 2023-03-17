using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuh.Models;
using SpyDuh.Repositories;

namespace SpyDuh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpyController : ControllerBase
    {
        private readonly ISpyRepository _spyRepository;

        public SpyController(ISpyRepository spyRepository)
        {
            _spyRepository = spyRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_spyRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Post(Spy spy)
        {
            _spyRepository.Add(spy);
            // Why is "IActionResult Get()" needed for this to show that it works. It throws "error" within swagger but still posts??
            return CreatedAtAction("Get", new { id = spy.Id }, spy);
        }
    }
}
