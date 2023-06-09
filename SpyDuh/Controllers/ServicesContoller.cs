﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpyDuh.Repositories;
using SpyDuh.Models;
using Microsoft.Extensions.Hosting;
using SpyDuh.Utils;
using Microsoft.Identity.Client;

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

        [HttpGet("ListServices/{id}")]
        public IActionResult GetAll(int id)
        {
            return Ok(_servicesRepository.GetAll(id));
        }

        [HttpGet("{id}")]
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
        public IActionResult Post(ServiceWithoutCost service)
        {
            _servicesRepository.Add(service);
            return CreatedAtAction("Get", new { id = service.Id }, service );
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ServiceWithoutCost service)
        {
            if (id != service.Id)
            {
                return BadRequest();
            }

            _servicesRepository.Update(service);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _servicesRepository.Delete(id);
            return NoContent();
        }
    }
}
