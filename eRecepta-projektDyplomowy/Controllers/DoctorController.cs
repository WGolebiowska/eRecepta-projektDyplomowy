using AutoMapper;
using eRecepta_projektDyplomowy.Controllers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eRecepta_projektDyplomowy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : BaseController
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IMapper mapper, ILogger logger, IDoctorService doctorService) : base(mapper, logger)
        {
            _doctorService = doctorService;
        }
        // GET: api/<DoctorController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var doctors = _doctorService.GetDoctors();
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // GET api/<DoctorController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var doctor = _doctorService.GetDoctor(p => p.Id == id);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // POST api/<DoctorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DoctorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DoctorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
