using AutoMapper;
using eRecepta_projektDyplomowy.Controllers.Services.Interfaces;
using eRecepta_projektDyplomowy.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceStack.Host;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : BaseController
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly IAuthorizationService _authorizationService;

        public PrescriptionController(IMapper mapper, ILogger logger, IPrescriptionService prescriptionService, IAuthorizationService authorizationService) : base(mapper, logger)
        {
            _authorizationService = authorizationService;
            _prescriptionService = prescriptionService;
        }
        // GET: api/<PrescriptionController>
        [HttpGet]
        [Authorize(Policy ="isAdmin")]
        public IActionResult Get()
        {
            try
            {
                var prescriptions = _prescriptionService.GetPrescriptions();
                return Ok(prescriptions);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // GET api/<PrescriptionController>/5
        [HttpGet("{id:int:min(1)}")]
        public IActionResult Get(int id)
        {
            try
            {
                var prescription = _prescriptionService.GetPrescription(p => p.PrescriptionId == id);
                return Ok(prescription);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("/api/Prescription/Get")]
        public async Task<IActionResult> GetAsync([FromQuery(Name = "patientId")] string patientId = null, [FromQuery(Name = "doctorId")] string doctorId = null)
        {
            try
            {
                var prescriptions = new List<PrescriptionVm>();
                if(doctorId != null && patientId != null)
                {
                    prescriptions = (List<PrescriptionVm>)_prescriptionService.GetPrescriptions(a => a.DoctorId == doctorId && a.PatientId == patientId);
                } 
                else if(doctorId != null)
                {
                    prescriptions = (List<PrescriptionVm>)_prescriptionService.GetPrescriptions(a => a.DoctorId == doctorId);
                }
                else if (patientId != null)
                {
                    prescriptions = (List<PrescriptionVm>)_prescriptionService.GetPrescriptions(a => a.PatientId == patientId);
                }
                else
                {
                    throw new HttpException(404, "Nie znaleziono danych o podanych parametrach");
                }

                var authorizationResult = await _authorizationService.AuthorizeAsync(User, patientId, "GetPrescriptionsPolicy");
                if (authorizationResult.Succeeded)
                {
                    return Ok(prescriptions);
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

        // POST api/<PrescriptionController>
        [HttpPost]
        [Authorize(Policy="isDoctor")]
        public IActionResult Post([FromBody] AddOrUpdatePrescriptionVm addOrUpdatePrescriptionVm)
        {
            return PostOrPutHelper(addOrUpdatePrescriptionVm);
        }

        // PUT api/<PrescriptionController>/5
        [HttpPut]
        [Authorize(Policy="isDoctor")]
        public IActionResult Put([FromBody] AddOrUpdatePrescriptionVm addOrUpdatePrescriptionVm)
        {
            return PostOrPutHelper(addOrUpdatePrescriptionVm);
        }

        // DELETE api/<PrescriptionController>/5
        [HttpDelete("{id:int:min(1)}")]
        [Authorize(Policy = "isDoctor")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _prescriptionService.DeletePrescription(p => p.PrescriptionId == id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        private IActionResult PostOrPutHelper(AddOrUpdatePrescriptionVm addOrUpdatePrescriptionVm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(_prescriptionService.AddOrUpdatePrescription(addOrUpdatePrescriptionVm));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return StatusCode(500, "Error occured");
            }
        }
    }
}
