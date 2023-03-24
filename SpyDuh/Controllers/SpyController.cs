using System;
using Microsoft.AspNetCore.Mvc;
using SpyDuh.Repositories;
using SpyDuh.Models;

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

        [HttpGet("{id}/ListEnemies")]
        public IActionResult ListEnemies(int id)
        {
            var enemies = _spyRepository.listEnemies(id);
            if (enemies == null)
            {
                return NotFound();
            }
            return Ok(enemies);
        }

        [HttpGet("{id}/Enemies")]
        public IActionResult GetEnemies(int id)
        {
            var enemies = _spyRepository.getEnemies(id);
            if (enemies == null)
            {
                return NotFound();
            }
            return Ok(enemies);
        }

        [ActionName("test")]
        [HttpPost]
        public IActionResult Post(NewSpy spy)
        {
            if(spy.HandlerId == 0)
            {
                return BadRequest("Please enter a valid HandlerId.");
            }
            _spyRepository.Add(spy);
            return CreatedAtAction("test", new { id = spy.Id }, spy);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _spyRepository.Delete(id);
            return NoContent();
        }

        [HttpPost("{spyId}/AddEnemy")]
        public IActionResult Post(int spyId, int enemyId)
        {
            _spyRepository.AddEnemy(spyId, enemyId);
            return NoContent();
        }
        [HttpGet("{id}/Friends")]
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
