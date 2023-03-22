using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuh.Models;
using SpyDuh.Repositories;

namespace SpyDuh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsRepository _skillsRepository;

        public SkillsController(ISkillsRepository skillsRepository)
        {
            _skillsRepository = skillsRepository;
        }

        [HttpGet("search")]
        public IActionResult Search(string skill)
        {
            return Ok(_skillsRepository.GetSpecificSkills(skill));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, SkillJoin skillJoin)
        {
            if(id != skillJoin.Id)
            {
                return BadRequest();
            }
            _skillsRepository.UpdateSkill(skillJoin);
            return NoContent();
        }
    }
}
