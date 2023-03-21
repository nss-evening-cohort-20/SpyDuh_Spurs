using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuh.Repositories;

namespace SpyDuh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IFriendRepository _friendRepository;

        public FriendController(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) 
        {
            var friends = _friendRepository.GetFriends(id);
            if (friends == null)
            {
                return NotFound();
            }
            return Ok(friends);
        }
    }
}
