using AutoMapper;
using eRecepta_projektDyplomowy.Controllers.Services.Interfaces;
using eRecepta_projektDyplomowy.Data;
using eRecepta_projektDyplomowy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace eRecepta_projektDyplomowy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : BaseController
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IMapper mapper, ILogger logger, IAppointmentService appointmentService) : base(mapper, logger)
        {
            _appointmentService = appointmentService;
        }
        // GET: api/<AppointmentController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var appointments = _appointmentService.GetAppointments();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // GET api/<AppointmentController>/5
        [HttpGet("{id:int:min(1)}")]
        public IActionResult Get(int id)
        {
            try
            {
                var appointment = _appointmentService.GetAppointment(p => p.AppointmentId == id);
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // POST api/<AppointmentController>
        [HttpPost]
        public IActionResult Post([FromBody] AddOrUpdateAppointmentVm addOrUpdateAppointmentVm)
        {
            return PostOrPutHelper(addOrUpdateAppointmentVm);
        }

        // PUT api/<AppointmentController>/5
        [HttpPut]
        public IActionResult Put([FromBody] AddOrUpdateAppointmentVm addOrUpdateAppointmentVm)
        {
            return PostOrPutHelper(addOrUpdateAppointmentVm);
        }

        // DELETE api/<AppointmentController>/5
        [HttpDelete("{id:int:min(1)}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _appointmentService.DeleteAppointment(p => p.AppointmentId == id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        private IActionResult PostOrPutHelper(AddOrUpdateAppointmentVm addOrUpdateAppointmentVm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(_appointmentService.AddOrUpdateAppointment(addOrUpdateAppointmentVm));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return StatusCode(500, "Error occured");
            }
        }
    }
}
