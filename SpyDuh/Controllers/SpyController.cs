using Microsoft.AspNetCore.Mvc;
using SpyDuh.Repositories;
using SpyDuh.Models;

namespace SpyDuh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpyController: ControllerBase
    {
        private readonly ISpyRepository _spyRepository;

        public SpyController(ISpyRepository spyRepository)
        {
            _spyRepository = spyRepository;
        }

        [HttpGet]
        public IActionResult GetEnemies(int id) 
        { 
            var enemies = _spyRepository.getEnemies(id);
            if(enemies == null)
            {
                return NotFound();
            }
            return Ok(enemies);

        }
        [HttpGet]
        public IActionResult GetFriends(int id)
        {
            var friends = _spyRepository.getFriends(id);
            if (friends == null)
            {
                return NotFound();
            }
            return Ok(friends);
        }
    }
}
