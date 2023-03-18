using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{skill}")]
        public IActionResult Get(string skill)
        {
            var skills = _skillsRepository.GetSpecificSkills(skill);
            if(skills == null)
            {
                return NotFound();
            }
            return Ok(skills);
        }
    }
}
