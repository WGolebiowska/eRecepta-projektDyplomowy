using AutoMapper;
using eRecepta_projektDyplomowy.Controllers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eRecepta_projektDyplomowy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : BaseController
    {
        private readonly IPatientService _patientService;
        private readonly IAuthorizationService _authorizationService;

        public PatientController(IMapper mapper, ILogger logger, IPatientService patientService, IAuthorizationService authorizationService) : base(mapper, logger)
        {
            _patientService = patientService;
            _authorizationService = authorizationService;
        }
        // GET: api/<PatientController>
        [Authorize(Policy = "isAdmin")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var patients = _patientService.GetPatients();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // GET api/<PatientController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            try
            {
                var patient = _patientService.GetPatient(p => p.Id == id);
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, "isAdminOrDoctor");
                if (authorizationResult.Succeeded)
                {
                    return Ok(patient);
                }
                else if (User.Identity.IsAuthenticated)
                {
                    return new ForbidResult();
                }
                else
                {
                    return new ChallengeResult();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // POST api/<PatientController>
        [HttpPost]
        [Authorize(Policy ="isAdmin")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PatientController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "isAdmin")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PatientController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "isAdmin")]
        public void Delete(int id)
        {
        }
    }
}
