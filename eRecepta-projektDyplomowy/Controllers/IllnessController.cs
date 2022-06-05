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
    public class IllnessController : BaseController
    {
        private readonly IIllnessService _illnessService;

        public IllnessController(IMapper mapper, ILogger logger, IIllnessService illnessService, IAuthorizationService authorizationService) : base(mapper, logger)
        {
            _illnessService = illnessService;
        }
        // GET: api/<IllnessController>
        [HttpGet]

        public IActionResult Get()
        {
            try
            {
                var illnesses = _illnessService.GetIllnesses();
                return Ok(illnesses);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // GET api/<IllnessController>/5
        [HttpGet("{id:int:min(1)}")]
        public IActionResult Get(int id)
        {
            try
            {
                var illness = _illnessService.GetIllness(p => p.IllnessId == id);
                return Ok(illness);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // POST api/<IllnessController>
        [HttpPost]
        [Authorize(Policy = "isAdmin")]
        public IActionResult Post([FromBody] AddOrUpdateIllnessVm addOrUpdateIllnessVm)
        {
            return PostOrPutHelper(addOrUpdateIllnessVm);
        }

        // PUT api/<IllnessController>/5
        [HttpPut]
        public IActionResult Put([FromBody] AddOrUpdateIllnessVm addOrUpdateIllnessVm)
        {
            return PostOrPutHelper(addOrUpdateIllnessVm);
        }

        // DELETE api/<IllnessController>/5
        [HttpDelete("{id:int:min(1)}")]
        [Authorize(Policy = "isAdmin")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _illnessService.DeleteIllness(p => p.IllnessId == id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        private IActionResult PostOrPutHelper(AddOrUpdateIllnessVm addOrUpdateIllnessVm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(_illnessService.AddOrUpdateIllness(addOrUpdateIllnessVm));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return StatusCode(500, "Error occured");
            }
        }
    }
}