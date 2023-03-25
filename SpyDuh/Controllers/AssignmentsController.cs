using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuh.Repositories;

namespace SpyDuh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        public readonly IAssignmentRepository _assignmentRepository;

        public AssignmentsController(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) 
        { 
            var assign = _assignmentRepository.GetAssignment(id);
            if (assign == null)
            {
                return NotFound();
            }
            return Ok(assign);
        }
    }
}
