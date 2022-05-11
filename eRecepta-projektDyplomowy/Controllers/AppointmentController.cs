using AutoMapper;
using eRecepta_projektDyplomowy.Controllers.Services.Interfaces;
using eRecepta_projektDyplomowy.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceStack.Host;
using System;

namespace eRecepta_projektDyplomowy.Controllers
{
    [Authorize]
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
        [Authorize(Policy ="isAdmin")]
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
        [Authorize(Policy = "isAdmin")]
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

        [HttpGet]
        [Route("/api/Appointment/Get")]
        public IActionResult Get([FromQuery(Name = "patientId")] string patientId = null, [FromQuery(Name = "doctorId")] string doctorId = null)
        {
            try
            {
                if(doctorId != null && patientId != null)
                {
                    var appointments = _appointmentService.GetAppointments(a => a.DoctorId == doctorId && a.PatientId == patientId);
                    return Ok(appointments);
                } 
                else if(doctorId != null)
                {
                    var appointments = _appointmentService.GetAppointments(a => a.DoctorId == doctorId);
                    return Ok(appointments);
                }
                else if (patientId != null)
                {
                    var appointments = _appointmentService.GetAppointments(a => a.PatientId == patientId);
                    return Ok(appointments);
                }
                else
                {
                    throw new HttpException(404, "Nie znaleziono danych o podanych parametrach");
                }
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
        [Authorize(Policy = "isAdmin")]
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
