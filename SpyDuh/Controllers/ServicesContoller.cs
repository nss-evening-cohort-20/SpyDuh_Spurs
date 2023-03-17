using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuh.Repositories;
using SpyDuh.Models;
using Microsoft.Extensions.Hosting;
using SpyDuh.Utils;

namespace SpyDuh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesContoller : ControllerBase
    {
        private readonly IServicesRepository _servicesRepository;

        public ServicesContoller(IServicesRepository servicesRepository)
        {
            _servicesRepository = servicesRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_servicesRepository.GetAll());
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int id)
        {
            var service = _servicesRepository.GetById(id);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        [HttpPost]
        public IActionResult Post(Service service)
        {
            _servicesRepository.Add(service);
            return CreatedAtAction("Get", new { id = service.Id }, service );
        }
    }
}
