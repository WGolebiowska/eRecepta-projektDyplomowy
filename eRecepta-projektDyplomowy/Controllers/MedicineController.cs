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
    public class MedicineController : BaseController
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMapper mapper, ILogger logger, IMedicineService medicineService, IAuthorizationService authorizationService) : base(mapper, logger)
        {
            _medicineService = medicineService;
        }
        // GET: api/<MedicineController>
        [HttpGet]

        public IActionResult Get()
        {
            try
            {
                var medicines = _medicineService.GetMedicines();
                return Ok(medicines);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // GET api/<MedicineController>/5
        [HttpGet("{id:int:min(1)}")]
        public IActionResult Get(int id)
        {
            try
            {
                var medicine = _medicineService.GetMedicine(p => p.MedicineId == id);
                return Ok(medicine);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // POST api/<MedicineController>
        [HttpPost]
        [Authorize(Policy = "isAdmin")]
        public IActionResult Post([FromBody] AddOrUpdateMedicineVm addOrUpdateMedicineVm)
        {
            return PostOrPutHelper(addOrUpdateMedicineVm);
        }

        // PUT api/<MedicineController>/5
        [HttpPut]
        public IActionResult Put([FromBody] AddOrUpdateMedicineVm addOrUpdateMedicineVm)
        {
            return PostOrPutHelper(addOrUpdateMedicineVm);
        }

        // DELETE api/<MedicineController>/5
        [HttpDelete("{id:int:min(1)}")]
        [Authorize(Policy = "isAdmin")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _medicineService.DeleteMedicine(p => p.MedicineId == id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        private IActionResult PostOrPutHelper(AddOrUpdateMedicineVm addOrUpdateMedicineVm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(_medicineService.AddOrUpdateMedicine(addOrUpdateMedicineVm));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return StatusCode(500, "Error occured");
            }
        }
    }
}