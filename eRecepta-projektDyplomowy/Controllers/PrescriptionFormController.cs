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
    public class PrescriptionFormController : BaseController
    {
        private readonly IPrescriptionFormService _prescriptionFormService;
        private readonly IAuthorizationService _authorizationService;

        public PrescriptionFormController(IMapper mapper, ILogger logger, IPrescriptionFormService prescriptionFormService, IAuthorizationService authorizationService) : base(mapper, logger)
        {
            _authorizationService = authorizationService;
            _prescriptionFormService = prescriptionFormService;
        }
        // GET: api/<PrescriptionFormController>
        [HttpGet]
        [Authorize(Policy = "isDoctor")]
        public IActionResult Get()
        {
            try
            {
                var prescriptionForms = _prescriptionFormService.GetPrescriptionForms();
                return Ok(prescriptionForms);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // GET api/<PrescriptionFormController>/5
        [HttpGet("{id:int:min(1)}")]
        [Authorize(Policy = "isDoctor")]
        public IActionResult Get(int id)
        {
            try
            {
                var prescriptionForm = _prescriptionFormService.GetPrescriptionForm(p => p.PrescriptionFormId == id);
                return Ok(prescriptionForm);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // POST api/<PrescriptionFormController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] AddOrUpdatePrescriptionFormVm addOrUpdatePrescriptionFormVm)
        {
            return PostOrPutHelper(addOrUpdatePrescriptionFormVm);
        }

        // PUT api/<PrescriptionFormController>/5
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] AddOrUpdatePrescriptionFormVm addOrUpdatePrescriptionFormVm)
        {
            return PostOrPutHelper(addOrUpdatePrescriptionFormVm);
        }

        // DELETE api/<PrescriptionFormController>/5
        [HttpDelete("{id:int:min(1)}")]
        [Authorize(Policy = "isAdmin")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _prescriptionFormService.DeletePrescriptionForm(p => p.PrescriptionFormId == id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        private IActionResult PostOrPutHelper(AddOrUpdatePrescriptionFormVm addOrUpdatePrescriptionFormVm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(_prescriptionFormService.AddOrUpdatePrescriptionForm(addOrUpdatePrescriptionFormVm));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return StatusCode(500, "Error occured");
            }
        }
    }
}
